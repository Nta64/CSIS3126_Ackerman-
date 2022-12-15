-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 15, 2022 at 05:51 PM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `audioplaylist`
--

-- --------------------------------------------------------

--
-- Table structure for table `appsongs`
--

CREATE TABLE `appsongs` (
  `Song_id` int(11) NOT NULL,
  `Title` varchar(200) NOT NULL,
  `Artist` varchar(200) NOT NULL,
  `Album` varchar(200) NOT NULL,
  `Url` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `appsongs`
--

INSERT INTO `appsongs` (`Song_id`, `Title`, `Artist`, `Album`, `Url`) VALUES
(1, 'Bad, Bad, Leroy Brown', 'Jim Croce', 'http://192.168.1.152:8888/BadBadLeyroy.jpg', 'http://192.168.1.152:8888/SongOne.mp3'),
(2, 'Dont Fear The Reaper', 'Blue Oyster Cult', 'http://192.168.1.152:8888/DontFearThe.jpg', 'http://192.168.1.152:8888/SongTwo.mp3'),
(3, 'Could You Be Loved', 'Bob Marley', 'http://192.168.1.152:8888/CouldYouBe.jpg', 'http://192.168.1.152:8888/SongThree.mp3'),
(4, 'Hotel California', 'Eagles', 'http://192.168.1.152:8888/HotelCalifornia.jpg', 'http://192.168.1.152:8888/SongFour.mp3'),
(5, 'Saturday in the Park', 'Chicago', 'http://192.168.1.152:8888/SaturdayInThe.jpg', 'http://192.168.1.152:8888/SongFive.mp3'),
(6, 'Paint It, Black', 'Rolling Stones', 'http://192.168.1.152:8888/PaintItBlack.jpg', 'http://192.168.1.152:8888/SongSix.mp3'),
(7, 'Long Cool Woman in a Black Dress', 'Hollies', 'http://192.168.1.152:8888/LongCoolWoman.jpg', 'http://192.168.1.152:8888/SongSeven.mp3'),
(8, 'Shining Star', 'JEarth, Wind & Fire', 'http://192.168.1.152:8888/ShiningStar.jpg', 'http://192.168.1.152:8888/SongEight.mp3'),
(9, 'American Woman', 'The Guess Who', 'http://192.168.1.152:8888/AmericanWoman.jpg', 'http://192.168.1.152:8888/SongNine.mp3');

-- --------------------------------------------------------

--
-- Table structure for table `playlists`
--

CREATE TABLE `playlists` (
  `Playlist_id` int(11) NOT NULL,
  `User_id` int(11) NOT NULL,
  `Name` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `playlists`
--

INSERT INTO `playlists` (`Playlist_id`, `User_id`, `Name`) VALUES
(18, 19, 'Playlist1'),
(19, 19, 'Playlist1'),
(21, 22, 'Playlist1'),
(24, 24, 'Playlist1');

-- --------------------------------------------------------

--
-- Table structure for table `playlist_songs`
--

CREATE TABLE `playlist_songs` (
  `Song_id` int(11) NOT NULL,
  `Playlist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `playlist_songs`
--

INSERT INTO `playlist_songs` (`Song_id`, `Playlist_id`) VALUES
(6, 18),
(7, 18),
(3, 18),
(5, 24),
(3, 24),
(5, 18),
(6, 18);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `UserName` varchar(200) NOT NULL,
  `password` varbinary(256) NOT NULL,
  `DateTime` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `UserName`, `password`, `DateTime`) VALUES
(19, 'UserOne', 0x303331303437303546303530304346303844304638303532304543303833304439303532304345304232303332303945304344304444303444303339304643303530303639303941304338304145303630303434303644303346304430304646, '2022-12-15 12:17:49'),
(24, 'UserTwo', 0x304237304337304643304438303646304245303839303941304145303941303732304230303138304638303531303130303434303734303241303339303036303838304537303732304335303946303531304638303333303530304246303131, '2022-12-15 12:35:42');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `appsongs`
--
ALTER TABLE `appsongs`
  ADD PRIMARY KEY (`Song_id`);

--
-- Indexes for table `playlists`
--
ALTER TABLE `playlists`
  ADD PRIMARY KEY (`Playlist_id`);

--
-- Indexes for table `playlist_songs`
--
ALTER TABLE `playlist_songs`
  ADD KEY `Song_id` (`Song_id`),
  ADD KEY `Playlist_id` (`Playlist_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `userName` (`UserName`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `appsongs`
--
ALTER TABLE `appsongs`
  MODIFY `Song_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `playlists`
--
ALTER TABLE `playlists`
  MODIFY `Playlist_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `playlist_songs`
--
ALTER TABLE `playlist_songs`
  ADD CONSTRAINT `playlist_songs_ibfk_1` FOREIGN KEY (`Song_id`) REFERENCES `appsongs` (`Song_id`),
  ADD CONSTRAINT `playlist_songs_ibfk_2` FOREIGN KEY (`Playlist_id`) REFERENCES `playlists` (`Playlist_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
