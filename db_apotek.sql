/*
SQLyog Community v13.1.8 (64 bit)
MySQL - 8.0.27 : Database - db_apotek
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`db_apotek` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `db_apotek`;

/*Table structure for table `__efmigrationshistory` */

DROP TABLE IF EXISTS `__efmigrationshistory`;

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `__efmigrationshistory` */

insert  into `__efmigrationshistory`(`MigrationId`,`ProductVersion`) values 
('20211227033124_initial','2.2.6-servicing-10079');

/*Table structure for table `obats` */

DROP TABLE IF EXISTS `obats`;

CREATE TABLE `obats` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Kode` longtext,
  `Nama` longtext,
  `Stok` int NOT NULL,
  `Harga` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `obats` */

insert  into `obats`(`Id`,`Kode`,`Nama`,`Stok`,`Harga`) values 
(1,'111','Paracetamol',5,500),
(2,'222','Amoxicillin',8,300),
(3,'tes1','add',2,100);

/*Table structure for table `transaksidetails` */

DROP TABLE IF EXISTS `transaksidetails`;

CREATE TABLE `transaksidetails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TransaksiId` int DEFAULT NULL,
  `ObatId` int DEFAULT NULL,
  `Jumlah` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_TransaksiDetails_Obats_ObatsiId_idx` (`ObatId`),
  KEY `FK_TransaksiDetails_Transaksis_TransaksiId_idx` (`TransaksiId`),
  CONSTRAINT `FK_TransaksiDetails_Obats_ObatsiId` FOREIGN KEY (`ObatId`) REFERENCES `obats` (`Id`),
  CONSTRAINT `FK_TransaksiDetails_Transaksis_TransaksiId` FOREIGN KEY (`TransaksiId`) REFERENCES `transaksis` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `transaksidetails` */

insert  into `transaksidetails`(`Id`,`TransaksiId`,`ObatId`,`Jumlah`) values 
(1,1,1,2),
(2,1,2,1),
(3,2,2,2);

/*Table structure for table `transaksis` */

DROP TABLE IF EXISTS `transaksis`;

CREATE TABLE `transaksis` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Kode` longtext,
  `Total` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `transaksis` */

insert  into `transaksis`(`Id`,`Kode`,`Total`) values 
(1,'T271220215',1300),
(2,'T271220217',600);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
