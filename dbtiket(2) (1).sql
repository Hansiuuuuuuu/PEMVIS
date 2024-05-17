-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 17, 2024 at 07:31 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbtiket`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbgerbong`
--

CREATE TABLE `tbgerbong` (
  `kodegerbong` int(100) NOT NULL,
  `gerbong` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbgerbong`
--

INSERT INTO `tbgerbong` (`kodegerbong`, `gerbong`) VALUES
(5, 'GERBONG1'),
(7, 'GERBONG3');

-- --------------------------------------------------------

--
-- Table structure for table `tbkereta`
--

CREATE TABLE `tbkereta` (
  `kode` int(11) NOT NULL,
  `asal` varchar(255) NOT NULL,
  `tujuan` varchar(255) NOT NULL,
  `harga` int(11) NOT NULL,
  `kelas` varchar(255) NOT NULL,
  `jam` date NOT NULL,
  `gerbong` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbkereta`
--

INSERT INTO `tbkereta` (`kode`, `asal`, `tujuan`, `harga`, `kelas`, `jam`, `gerbong`) VALUES
(27, 'SAMARINDA', 'BALIKPAPAN', 500000, 'Ekonomi', '2024-05-17', 'GERBONG3');

-- --------------------------------------------------------

--
-- Table structure for table `tbpesanan`
--

CREATE TABLE `tbpesanan` (
  `id_pesan` int(11) NOT NULL,
  `nama` text NOT NULL,
  `nohp` int(11) NOT NULL,
  `asal` varchar(255) NOT NULL,
  `tujuann` text NOT NULL,
  `kelass` text NOT NULL,
  `hargaa` int(11) NOT NULL,
  `jamm` date NOT NULL,
  `gerbongg` text NOT NULL,
  `id_user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbpesanan`
--

INSERT INTO `tbpesanan` (`id_pesan`, `nama`, `nohp`, `asal`, `tujuann`, `kelass`, `hargaa`, `jamm`, `gerbongg`, `id_user`) VALUES
(6, 'uding', 92338, 'SAMARINDA', 'BALIKPAPAN', 'Ekonomi', 500000, '2024-05-18', 'GERBONG3', 2);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id_user` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id_user`, `username`, `password`) VALUES
(1, 'admin', 'admin'),
(2, 'user', 'user'),
(3, 'user2', 'user');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbgerbong`
--
ALTER TABLE `tbgerbong`
  ADD PRIMARY KEY (`kodegerbong`);

--
-- Indexes for table `tbkereta`
--
ALTER TABLE `tbkereta`
  ADD PRIMARY KEY (`kode`),
  ADD KEY `gerbong` (`gerbong`);

--
-- Indexes for table `tbpesanan`
--
ALTER TABLE `tbpesanan`
  ADD PRIMARY KEY (`id_pesan`),
  ADD KEY `id_user` (`id_user`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbgerbong`
--
ALTER TABLE `tbgerbong`
  MODIFY `kodegerbong` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `tbkereta`
--
ALTER TABLE `tbkereta`
  MODIFY `kode` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `tbpesanan`
--
ALTER TABLE `tbpesanan`
  MODIFY `id_pesan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tbpesanan`
--
ALTER TABLE `tbpesanan`
  ADD CONSTRAINT `tbpesanan_ibfk_1` FOREIGN KEY (`id_user`) REFERENCES `user` (`id_user`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
