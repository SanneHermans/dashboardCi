-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 06 jun 2023 om 13:19
-- Serverversie: 10.4.17-MariaDB
-- PHP-versie: 8.0.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `robot4care`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `emergency`
--

CREATE TABLE `emergency` (
  `ID` int(11) NOT NULL,
  `Emergency` tinyint(1) NOT NULL,
  `Created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `emergencylocations`
--

CREATE TABLE `emergencylocations` (
  `ID` int(11) NOT NULL,
  `Location_id` int(11) NOT NULL,
  `Created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `james`
--

CREATE TABLE `james` (
  `ID` int(11) NOT NULL,
  `Location_id` int(11) NOT NULL,
  `Created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `locations`
--

CREATE TABLE `locations` (
  `ID` int(10) NOT NULL,
  `RouteID` int(10) DEFAULT NULL,
  `X` double NOT NULL,
  `Y` double NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `routes`
--

CREATE TABLE `routes` (
  `ID` int(11) NOT NULL,
  `Name` tinytext NOT NULL,
  `Created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `Updated_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `emergency`
--
ALTER TABLE `emergency`
  ADD PRIMARY KEY (`ID`);

--
-- Indexen voor tabel `emergencylocations`
--
ALTER TABLE `emergencylocations`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Location_id` (`Location_id`);

--
-- Indexen voor tabel `james`
--
ALTER TABLE `james`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Location_id` (`Location_id`);

--
-- Indexen voor tabel `locations`
--
ALTER TABLE `locations`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `RouteID` (`RouteID`);

--
-- Indexen voor tabel `routes`
--
ALTER TABLE `routes`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `emergency`
--
ALTER TABLE `emergency`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `emergencylocations`
--
ALTER TABLE `emergencylocations`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `james`
--
ALTER TABLE `james`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `locations`
--
ALTER TABLE `locations`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `routes`
--
ALTER TABLE `routes`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `emergencylocations`
--
ALTER TABLE `emergencylocations`
  ADD CONSTRAINT `emergencylocations_ibfk_1` FOREIGN KEY (`Location_id`) REFERENCES `locations` (`ID`);

--
-- Beperkingen voor tabel `james`
--
ALTER TABLE `james`
  ADD CONSTRAINT `james_ibfk_1` FOREIGN KEY (`Location_id`) REFERENCES `locations` (`ID`) ON DELETE CASCADE;

--
-- Beperkingen voor tabel `locations`
--
ALTER TABLE `locations`
  ADD CONSTRAINT `locations_ibfk_1` FOREIGN KEY (`RouteID`) REFERENCES `routes` (`ID`) ON DELETE SET NULL ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
