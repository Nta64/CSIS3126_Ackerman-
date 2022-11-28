﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Audio
{
    public class Timer

    {

        public TimeSpan _timeSpan;

        public Action _callback;




        private static CancellationTokenSource _cancellationTokenSource;




        public Timer(TimeSpan timeSpan, Action callback)

        {

            _timeSpan = timeSpan;

            _callback = callback;

            _cancellationTokenSource = new CancellationTokenSource();

        }

        public void Start()

        {

            CancellationTokenSource cts = _cancellationTokenSource; // safe copy

            Device.StartTimer(_timeSpan, () =>

            {

                if (cts.IsCancellationRequested)

                {

                    return false;

                }

                _callback.Invoke();

                return true; //true to continuous, false to single use

            });

        }




        public void Stop()

        {

            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();

        }
    }
}