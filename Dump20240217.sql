-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cwbmaster
-- ------------------------------------------------------
-- Server version	8.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `cwbmaster`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cwbmaster` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `cwbmaster`;

--
-- Table structure for table `BaseRawMaterials`
--

DROP TABLE IF EXISTS `BaseRawMaterials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `BaseRawMaterials` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `BaseRawMaterial_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BaseRawMaterials`
--

LOCK TABLES `BaseRawMaterials` WRITE;
/*!40000 ALTER TABLE `BaseRawMaterials` DISABLE KEYS */;
INSERT INTO `BaseRawMaterials` VALUES (30,'Steel',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(31,'Brass',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(32,'Cast Iron',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(33,'Aluminium',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(34,'rod',0,'2023-11-21 06:05:22','2023-11-21 06:05:22','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `BaseRawMaterials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BoughtOutFinishDetails`
--

DROP TABLE IF EXISTS `BoughtOutFinishDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `BoughtOutFinishDetails` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `BoughtOutFinishMadeType` int DEFAULT NULL,
  `PartId` bigint NOT NULL,
  `SupplierPartNo` varchar(200) NOT NULL,
  `AdditionalInfo` varchar(400) DEFAULT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL,
  `LastModifiedDate` datetime NOT NULL,
  `Creator` varchar(450) NOT NULL,
  `Modifier` varchar(450) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `PartId` (`PartId`),
  CONSTRAINT `BoughtOutFinishDetails_ibfk_1` FOREIGN KEY (`PartId`) REFERENCES `MasterParts` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BoughtOutFinishDetails`
--

LOCK TABLES `BoughtOutFinishDetails` WRITE;
/*!40000 ALTER TABLE `BoughtOutFinishDetails` DISABLE KEYS */;
INSERT INTO `BoughtOutFinishDetails` VALUES (1,2,5,'1000987','information',1,'2023-12-10 02:05:06','2023-12-10 03:41:24','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,2,18,'10009','information',1,'2023-12-16 01:52:35','2023-12-16 01:52:35','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,2,20,'10009','information',1,'2023-12-17 22:51:08','2023-12-17 22:51:08','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(14,2,1,'10009','information',1,'2023-12-18 00:27:30','2023-12-18 00:27:30','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(15,2,21,'10098','Information',1,'2023-12-18 00:28:03','2023-12-18 00:28:03','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(16,1,27,'123432','dsafasd',1,'2024-01-26 08:29:37','2024-01-26 08:29:37','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `BoughtOutFinishDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Companies`
--

DROP TABLE IF EXISTS `Companies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Companies` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Type` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Company_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=134 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Companies`
--

LOCK TABLES `Companies` WRITE;
/*!40000 ALTER TABLE `Companies` DISABLE KEYS */;
INSERT INTO `Companies` VALUES (127,'Self','Both',1,'2023-12-02 14:20:26','2023-12-02 14:20:26','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(128,'Customer_001','Both',1,'2023-12-02 17:36:23','2023-12-02 17:36:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(129,'Customer_002','Both',1,'2023-12-04 20:35:45','2023-12-04 20:35:45','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(130,'Customer_003','Both',1,'2023-12-05 06:45:00','2023-12-05 06:45:00','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(131,'Customer_004','Both',1,'2023-12-07 11:31:46','2023-12-07 11:31:46','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(132,'supplier','Supplier',1,'2023-12-07 13:37:59','2023-12-07 13:37:59','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(133,'Customer_005','Customer',1,'2023-12-07 16:16:46','2023-12-07 16:16:46','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `Companies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Divisions`
--

DROP TABLE IF EXISTS `Divisions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Divisions` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Location` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Notes` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CompanyId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Division_CompanyId` (`CompanyId`),
  KEY `Division_TenantId` (`TenantId`),
  CONSTRAINT `FK_Divisions_Companies_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Divisions`
--

LOCK TABLES `Divisions` WRITE;
/*!40000 ALTER TABLE `Divisions` DISABLE KEYS */;
INSERT INTO `Divisions` VALUES (1,'Div1','Loc1','Notes...',127,1,'2023-12-02 14:20:27','2023-12-02 14:20:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,'Division_001','Location_001',NULL,128,1,'2023-12-02 17:36:23','2023-12-02 17:36:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,'Division_001','Location_001',NULL,129,1,'2023-12-04 20:35:45','2023-12-04 20:35:45','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,'Division_001','Location_001',NULL,130,1,'2023-12-05 06:45:00','2023-12-05 06:45:00','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,'Division_001','Location_001',NULL,131,1,'2023-12-07 11:31:46','2023-12-07 11:31:46','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,'dvi01','mys',NULL,132,1,'2023-12-07 13:37:59','2023-12-07 13:37:59','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,'Division_001','Location_001',NULL,133,1,'2023-12-07 16:16:46','2023-12-07 16:16:46','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `Divisions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MPBOMs`
--

DROP TABLE IF EXISTS `MPBOMs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MPBOMs` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PartId` bigint NOT NULL,
  `Quantity` bigint NOT NULL,
  `ManufPartId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `PartDescription` varchar(4000) DEFAULT NULL,
  `CreationDate` datetime NOT NULL,
  `LastModifiedDate` datetime NOT NULL,
  `Creator` varchar(450) NOT NULL,
  `Modifier` varchar(450) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `PartId` (`PartId`),
  KEY `ManufPartId` (`ManufPartId`),
  CONSTRAINT `MPBOMs_ibfk_1` FOREIGN KEY (`PartId`) REFERENCES `MasterParts` (`Id`),
  CONSTRAINT `MPBOMs_ibfk_2` FOREIGN KEY (`ManufPartId`) REFERENCES `ManufacturedPartNoDetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MPBOMs`
--

LOCK TABLES `MPBOMs` WRITE;
/*!40000 ALTER TABLE `MPBOMs` DISABLE KEYS */;
INSERT INTO `MPBOMs` VALUES (1,1,5000,2,1,'desc','2023-12-10 02:02:35','2023-12-10 02:02:35','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,1,5000,2,1,'desc','2023-12-10 02:02:40','2023-12-10 02:02:40','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,11,5000,16,1,'desc','2023-12-12 15:44:06','2023-12-12 15:44:06','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,11,5000,16,1,'desc','2023-12-12 15:44:09','2023-12-12 15:44:09','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,11,5000,16,1,'desc','2023-12-12 15:44:10','2023-12-12 15:44:10','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,11,5000,16,1,'desc','2023-12-12 15:44:13','2023-12-12 15:44:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,12,5000,17,1,'Description','2023-12-12 15:55:02','2023-12-12 15:55:02','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(8,12,5000,17,1,'Description','2023-12-12 15:55:06','2023-12-12 15:55:06','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(9,12,5000,17,1,'Description','2023-12-12 15:55:10','2023-12-12 15:55:18','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(10,13,6000,17,1,'Description','2023-12-22 11:31:14','2023-12-22 11:31:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(11,11,1000,2,1,'desc','2024-01-14 12:57:45','2024-01-14 12:57:45','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(12,10,2000,16,1,'desc','2024-01-25 12:29:03','2024-01-25 12:29:03','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(13,7,1000,17,1,'desc','2024-01-25 13:14:12','2024-01-25 13:14:12','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(14,12,100,2,1,'Description','2024-01-25 15:57:01','2024-01-25 15:57:01','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `MPBOMs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MPRawMeterials`
--

DROP TABLE IF EXISTS `MPRawMeterials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MPRawMeterials` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PartId` bigint NOT NULL,
  `PartDescription` varchar(4000) DEFAULT NULL,
  `PartMadeFrom` bigint NOT NULL,
  `InputWeight` bigint NOT NULL,
  `ScrapGenerated` bigint NOT NULL,
  `QuantityPerInput` bigint NOT NULL,
  `YieldNotes` varchar(450) DEFAULT NULL,
  `PreferedRawMaterial` tinyint NOT NULL,
  `ManufPartId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL,
  `LastModifiedDate` datetime NOT NULL,
  `Creator` varchar(450) NOT NULL,
  `Modifier` varchar(450) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `PartId` (`PartId`),
  KEY `ManufPartId` (`ManufPartId`),
  CONSTRAINT `MPRawMeterials_ibfk_1` FOREIGN KEY (`PartId`) REFERENCES `MasterParts` (`Id`),
  CONSTRAINT `MPRawMeterials_ibfk_2` FOREIGN KEY (`ManufPartId`) REFERENCES `ManufacturedPartNoDetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MPRawMeterials`
--

LOCK TABLES `MPRawMeterials` WRITE;
/*!40000 ALTER TABLE `MPRawMeterials` DISABLE KEYS */;
INSERT INTO `MPRawMeterials` VALUES (1,1,'desc',3,1,1,1,'1',0,1,0,'2023-12-10 02:01:37','2023-12-10 02:01:37','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,7,'desc',0,2,1,2,'1',0,3,0,'2023-12-11 14:07:10','2023-12-11 14:07:18','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,8,'e3wd',0,1,1,1,'1',0,11,0,'2023-12-12 09:26:04','2023-12-12 09:26:31','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,8,'e3wd',3,2,2,2,'2',0,11,0,'2023-12-12 09:26:11','2023-12-12 09:26:11','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,8,'e3wd',3,3,3,3,'3',0,11,0,'2023-12-12 09:26:16','2023-12-12 09:26:16','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,8,'e3wd',0,4,4,5,'4',0,11,0,'2023-12-12 09:27:16','2023-12-12 09:27:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,8,'e3wd',0,6,6,8,'6',0,11,0,'2023-12-12 09:27:52','2023-12-12 09:27:58','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(9,13,'Description',3,2,2,2,'2',0,14,0,'2023-12-12 14:25:04','2023-12-12 14:25:04','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(10,13,'Description',3,3,3,3,'3',0,14,0,'2023-12-12 14:25:10','2023-12-12 14:25:10','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(11,13,'Description',0,6,4,6,'4',0,14,0,'2023-12-12 14:25:16','2023-12-12 14:39:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(13,12,'Description',3,3,3,3,'3',0,15,0,'2023-12-12 14:40:47','2023-12-12 14:40:47','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(14,12,'Description',3,4,4,4,'4',0,15,0,'2023-12-12 14:40:52','2023-12-12 14:40:52','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(15,12,'Description',3,5,5,5,'5',0,15,0,'2023-12-12 14:40:57','2023-12-12 14:40:57','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(16,12,'Description',3,6,6,6,'6',0,15,0,'2023-12-12 14:41:04','2023-12-12 14:41:04','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(17,8,'e3wd',3,2,2,2,'2',1,23,0,'2023-12-31 13:49:19','2023-12-31 13:49:19','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(18,8,'e3wd',3,2,2,2,'2',0,1,0,'2024-01-25 10:59:16','2024-01-25 10:59:16','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(19,10,'desc',3,2,2,2,'2',0,1,0,'2024-01-25 12:23:58','2024-01-25 12:23:58','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(20,10,'desc',3,3,3,3,'3',0,1,0,'2024-01-25 12:27:27','2024-01-25 12:27:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(21,10,'desc',3,2,2,2,'2',0,1,0,'2024-01-25 12:41:54','2024-01-25 12:41:54','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(22,17,'desc',3,2,2,2,'2',0,1,0,'2024-01-25 15:55:46','2024-01-25 15:55:46','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(23,12,'Description',3,2,2,2,'2',0,13,0,'2024-01-25 15:58:22','2024-01-25 15:58:22','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(24,14,'Description',3,2,2,2,'2',0,13,0,'2024-01-25 15:59:57','2024-01-25 15:59:57','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(25,13,'Description',3,3,3,3,'3',0,14,0,'2024-01-25 16:32:36','2024-01-25 16:32:36','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(26,7,'desc',3,1,1,1,'1',0,24,0,'2024-01-25 16:47:01','2024-01-25 16:47:01','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(27,7,'desc',3,2,2,2,'2',0,24,0,'2024-01-25 16:48:13','2024-01-25 16:48:13','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(28,17,'desc',3,3,3,3,'3',0,24,0,'2024-01-25 17:40:52','2024-01-25 17:40:52','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(29,25,'desc',0,4,2,4,'2',0,25,0,'2024-01-26 07:49:46','2024-01-26 07:50:19','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(30,25,'desc',3,3,3,3,'3',0,25,0,'2024-01-26 07:50:06','2024-01-26 07:50:06','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(32,34,'Desc',2,2,2,2,'2',0,32,0,'2024-01-26 18:48:46','2024-01-26 18:48:46','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `MPRawMeterials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MRAssemblyBOMs`
--

DROP TABLE IF EXISTS `MRAssemblyBOMs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MRAssemblyBOMs` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` bigint NOT NULL,
  `ManufacturingResourceId` bigint NOT NULL,
  `PartId` bigint NOT NULL,
  `Quantity` int NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `MRAssemblyBOM_ManufacturingResourceId` (`ManufacturingResourceId`),
  KEY `MRAssemblyBOM_TenantId` (`TenantId`),
  CONSTRAINT `FK_MRAssemblyBOMs_ManufacturingResources_ManufacturingResourceId` FOREIGN KEY (`ManufacturingResourceId`) REFERENCES `ManufacturingResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MRAssemblyBOMs`
--

LOCK TABLES `MRAssemblyBOMs` WRITE;
/*!40000 ALTER TABLE `MRAssemblyBOMs` DISABLE KEYS */;
/*!40000 ALTER TABLE `MRAssemblyBOMs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MachineProcessDocuments`
--

DROP TABLE IF EXISTS `MachineProcessDocuments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MachineProcessDocuments` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `DocumentTypeId` bigint NOT NULL,
  `IsMandatory` tinyint(1) NOT NULL DEFAULT '0',
  `TenantId` bigint NOT NULL,
  `MachineId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `MachineProcessDocument_DocumentTypeId` (`DocumentTypeId`),
  KEY `MachineProcessDocument_MachineId` (`MachineId`),
  KEY `MachineProcessDocument_TenantId` (`TenantId`),
  CONSTRAINT `FK_MachineProcessDocuments_Machines_MachineId` FOREIGN KEY (`MachineId`) REFERENCES `Machines` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MachineProcessDocuments`
--

LOCK TABLES `MachineProcessDocuments` WRITE;
/*!40000 ALTER TABLE `MachineProcessDocuments` DISABLE KEYS */;
INSERT INTO `MachineProcessDocuments` VALUES (1,4,1,1,1,'2023-12-29 12:56:29','2023-12-29 12:56:29','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,4,1,1,2,'2023-12-29 12:57:08','2023-12-29 12:57:08','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `MachineProcessDocuments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MachineTypes`
--

DROP TABLE IF EXISTS `MachineTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MachineTypes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `MachineType_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MachineTypes`
--

LOCK TABLES `MachineTypes` WRITE;
/*!40000 ALTER TABLE `MachineTypes` DISABLE KEYS */;
INSERT INTO `MachineTypes` VALUES (1,'test1',1,'2023-12-29 07:25:56','2023-12-29 07:25:56','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `MachineTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Machines`
--

DROP TABLE IF EXISTS `Machines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Machines` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Manufacturer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SlNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MachineTypeId` bigint NOT NULL,
  `PlantId` bigint NOT NULL,
  `ShopId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `OperationListId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Machine_MachineTypeId` (`MachineTypeId`),
  KEY `Machine_OperationListId` (`OperationListId`),
  KEY `Machine_PlantId` (`PlantId`),
  KEY `Machine_ShopId` (`ShopId`),
  KEY `Machine_TenantId` (`TenantId`),
  CONSTRAINT `FK_Machines_MachineTypes_MachineTypeId` FOREIGN KEY (`MachineTypeId`) REFERENCES `MachineTypes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Machines_OperationLists_OperationListId` FOREIGN KEY (`OperationListId`) REFERENCES `OperationLists` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Machines`
--

LOCK TABLES `Machines` WRITE;
/*!40000 ALTER TABLE `Machines` DISABLE KEYS */;
INSERT INTO `Machines` VALUES (1,'Machine1','Manuf1','0001',1,1,1,1,1,'2023-12-29 12:56:17','2023-12-29 12:56:17','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,'Machine2','Manuf1','0002',1,1,1,1,1,'2023-12-29 12:57:00','2023-12-29 12:57:00','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,'Plan1-Machine','Test','00098',1,5,1,1,1,'2024-01-03 05:51:21','2024-01-03 05:51:21','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `Machines` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ManufacturedPartNoDetails`
--

DROP TABLE IF EXISTS `ManufacturedPartNoDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ManufacturedPartNoDetails` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `ManufacturedPartType` bigint NOT NULL,
  `CompanyId` bigint NOT NULL,
  `PartId` bigint NOT NULL,
  `FinishedWeight` bigint DEFAULT NULL,
  `UOMId` bigint DEFAULT NULL,
  `TenantId` bigint DEFAULT NULL,
  `CreationDate` datetime DEFAULT NULL,
  `LastModifiedDate` datetime DEFAULT NULL,
  `Creator` varchar(450) DEFAULT NULL,
  `Modifier` varchar(450) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `PartId` (`PartId`),
  KEY `CompanyId` (`CompanyId`),
  CONSTRAINT `ManufacturedPartNoDetails_ibfk_1` FOREIGN KEY (`PartId`) REFERENCES `MasterParts` (`Id`),
  CONSTRAINT `ManufacturedPartNoDetails_ibfk_2` FOREIGN KEY (`CompanyId`) REFERENCES `Companies` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ManufacturedPartNoDetails`
--

LOCK TABLES `ManufacturedPartNoDetails` WRITE;
/*!40000 ALTER TABLE `ManufacturedPartNoDetails` DISABLE KEYS */;
INSERT INTO `ManufacturedPartNoDetails` VALUES (1,1,128,7,1,1,1,'2023-12-10 02:01:07','2023-12-10 03:52:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,2,128,2,1,1,1,'2023-12-10 02:02:19','2023-12-10 02:02:19','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,1,128,8,1,1,1,'2023-12-11 14:06:43','2023-12-11 14:06:43','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(11,1,128,10,1,1,1,'2023-12-12 09:25:12','2023-12-12 09:25:12','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(12,1,130,11,1,1,1,'2023-12-12 09:29:23','2023-12-12 09:29:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(13,1,131,12,1,1,1,'2023-12-12 14:23:12','2023-12-12 14:23:12','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(14,1,133,13,1,1,1,'2023-12-12 14:23:54','2023-12-12 14:23:54','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(15,1,131,14,2,1,1,'2023-12-12 14:40:23','2023-12-12 14:40:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(16,2,131,15,2,1,1,'2023-12-12 15:43:50','2023-12-12 15:43:50','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(17,2,129,16,1,1,1,'2023-12-12 15:54:40','2023-12-12 15:54:40','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(19,1,128,17,1,1,1,'2023-12-16 01:47:20','2023-12-16 01:47:20','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(21,1,128,19,1,1,1,'2023-12-17 02:16:49','2023-12-17 02:16:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(22,1,128,23,2,1,1,'2023-12-29 11:01:27','2023-12-29 11:01:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(23,1,128,3,2,1,1,'2023-12-31 13:48:50','2023-12-31 13:48:50','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(24,1,128,24,2,1,1,'2024-01-25 16:46:16','2024-01-25 16:46:16','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(25,1,129,25,120,1,1,'2024-01-26 07:48:31','2024-01-26 07:48:31','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(26,1,129,26,120,1,1,'2024-01-26 08:05:57','2024-01-26 08:05:57','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(27,1,128,28,2,1,1,'2024-01-26 17:47:27','2024-01-26 17:47:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(28,1,129,29,2,1,1,'2024-01-26 18:01:14','2024-01-26 18:01:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(29,1,127,30,2,1,1,'2024-01-26 18:26:40','2024-01-26 18:26:40','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(30,1,128,31,1,1,1,'2024-01-26 18:31:13','2024-01-26 18:31:13','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(31,1,128,33,120,1,1,'2024-01-26 18:39:08','2024-01-26 18:39:08','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(32,1,128,35,120,1,1,'2024-01-26 18:47:39','2024-01-26 18:47:39','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(33,1,129,36,2,1,1,'2024-01-26 18:55:40','2024-01-26 18:55:40','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(34,1,128,38,120,1,1,'2024-01-26 19:00:09','2024-01-26 19:00:09','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(35,1,128,39,120,1,1,'2024-01-26 23:12:49','2024-01-26 23:12:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `ManufacturedPartNoDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ManufacturingResourceGroups`
--

DROP TABLE IF EXISTS `ManufacturingResourceGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ManufacturingResourceGroups` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ParentManufacturingResourceId` bigint DEFAULT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `ManufacturingResourceGroup_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ManufacturingResourceGroups`
--

LOCK TABLES `ManufacturingResourceGroups` WRITE;
/*!40000 ALTER TABLE `ManufacturingResourceGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `ManufacturingResourceGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ManufacturingResourceSuppliers`
--

DROP TABLE IF EXISTS `ManufacturingResourceSuppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ManufacturingResourceSuppliers` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` bigint NOT NULL,
  `SupplierId` bigint NOT NULL,
  `ManufacturingResourceId` bigint NOT NULL,
  `DeliveryTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cost` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Notes` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `MOQ` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `ManufacturingResourceSupplier_ManufacturingResourceId` (`ManufacturingResourceId`),
  KEY `ManufacturingResourceSupplier_SupplierId` (`SupplierId`),
  KEY `ManufacturingResourceSupplier_TenantId` (`TenantId`),
  CONSTRAINT `FK_ManufacturingResourceSuppliers_Companies_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Companies` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ManufacturingResourceSuppliers_ManufacturingResources_Manufa~` FOREIGN KEY (`ManufacturingResourceId`) REFERENCES `ManufacturingResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ManufacturingResourceSuppliers`
--

LOCK TABLES `ManufacturingResourceSuppliers` WRITE;
/*!40000 ALTER TABLE `ManufacturingResourceSuppliers` DISABLE KEYS */;
/*!40000 ALTER TABLE `ManufacturingResourceSuppliers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ManufacturingResources`
--

DROP TABLE IF EXISTS `ManufacturingResources`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ManufacturingResources` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` bigint NOT NULL,
  `ManufacturingResourceGroupId` bigint NOT NULL,
  `MRItemType` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ItemDescription` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `StockLevel` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MRConsumptionType` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsPartsSpecificMRItem` tinyint(1) NOT NULL DEFAULT '0',
  `UOMId` bigint NOT NULL,
  `ReorderLevel` int NOT NULL,
  `NoOfTimesCanBeReused` int NOT NULL,
  `MRItemPartNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `IX_ManufacturingResources_UOMId` (`UOMId`),
  KEY `ManufacturingResource_ManufacturingResourceGroupId` (`ManufacturingResourceGroupId`),
  KEY `ManufacturingResource_TenantId` (`TenantId`),
  CONSTRAINT `FK_ManufacturingResources_ManufacturingResourceGroups_Manufactu~` FOREIGN KEY (`ManufacturingResourceGroupId`) REFERENCES `ManufacturingResourceGroups` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ManufacturingResources_UOMs_UOMId` FOREIGN KEY (`UOMId`) REFERENCES `UOMs` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ManufacturingResources`
--

LOCK TABLES `ManufacturingResources` WRITE;
/*!40000 ALTER TABLE `ManufacturingResources` DISABLE KEYS */;
/*!40000 ALTER TABLE `ManufacturingResources` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MasterParts`
--

DROP TABLE IF EXISTS `MasterParts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MasterParts` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PartNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PartDescription` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `MasterPartType` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Status` varchar(25) NOT NULL,
  `StatusChangeReason` varchar(300) DEFAULT NULL,
  `RevNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `RevDate` datetime DEFAULT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `PartNo` (`PartNo`),
  KEY `MasterPart_MasterPartType` (`MasterPartType`),
  KEY `MasterPart_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MasterParts`
--

LOCK TABLES `MasterParts` WRITE;
/*!40000 ALTER TABLE `MasterParts` DISABLE KEYS */;
INSERT INTO `MasterParts` VALUES (1,'1000987-A','desc','ManufacturedPart','Active',NULL,'1','2023-12-10 00:00:00',1,'2023-12-10 02:01:07','2023-12-10 02:01:07','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,'1000987-A33','desc','BOM','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-10 02:02:18','2023-12-10 02:02:18','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,'1000987','desc','RawMaterial','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-10 02:03:14','2023-12-10 02:03:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,'1000988','desc','BOF','Active',NULL,'2','0001-01-01 00:00:00',1,'2023-12-10 02:05:06','2023-12-10 03:29:34','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,'1000999','descinfo','BOF','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-10 03:39:09','2023-12-10 03:41:24','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,'100000','descinfo','RawMaterial','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-10 03:42:34','2023-12-11 13:37:12','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,'1000987-C','desc','ManufacturedPart','Active',NULL,'1','2023-12-10 00:00:00',1,'2023-12-10 03:52:13','2023-12-10 03:52:13','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(8,'1000987rew','e3wd','ManufacturedPart','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-11 14:06:42','2023-12-11 14:06:42','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(9,'1000987888','desc','RawMaterial','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-11 14:07:49','2023-12-11 14:07:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(10,'1000987899','desc','ManufacturedPart','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-12 09:24:48','2023-12-12 09:24:48','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(11,'131105','desc','ManufacturedPart','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-12 09:29:18','2023-12-12 09:29:18','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(12,'11315-AA','Description','ManufacturedPart','Active','131105','1','0001-01-01 00:00:00',1,'2023-12-12 14:23:08','2023-12-12 14:23:08','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(13,'11315-AAB','Description','ManufacturedPart','Active','131105','1','0001-01-01 00:00:00',1,'2023-12-12 14:23:51','2023-12-12 14:23:51','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(14,'11315-AAD','Description','ManufacturedPart','Active','131105','1','0001-01-01 00:00:00',1,'2023-12-12 14:40:23','2023-12-12 14:40:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(15,'11315-AAE','Description','BOM','Active','11315-AAD','1','0001-01-01 00:00:00',1,'2023-12-12 15:43:49','2023-12-12 15:43:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(16,'11305-AAF','Desc','BOM','Active','131105','1','0001-01-01 00:00:00',1,'2023-12-12 15:54:40','2023-12-12 15:54:40','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(17,'10009899','desc','ManufacturedPart','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-16 01:47:20','2023-12-16 01:47:20','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(18,'Test123','desc','BOF','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-16 01:52:09','2023-12-16 01:52:09','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(19,'1000987-PPA','Description','ManufacturedPart','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-17 02:16:49','2023-12-17 02:16:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(20,'100098','desc','BOF','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-17 22:51:05','2023-12-17 22:51:05','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(21,'1000987-N','Description','BOF','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-18 00:28:02','2023-12-18 00:28:02','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(22,'1000987-D','desc','RawMaterial','Active',NULL,'1','0001-01-01 00:00:00',1,'2023-12-18 00:29:44','2023-12-18 00:29:44','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(23,'1000987-k','desc','ManufacturedPart','Active',NULL,'1','2023-12-29 00:00:00',1,'2023-12-29 11:01:27','2023-12-29 11:01:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(24,'25012024','desc','ManufacturedPart','Active',NULL,'1','2024-01-25 00:00:00',1,'2024-01-25 16:46:15','2024-01-25 16:46:15','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(25,'26012024','desc','ManufacturedPart','Active',NULL,'1','2024-01-26 00:00:00',1,'2024-01-26 07:48:31','2024-01-26 07:48:31','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(26,'26012024-1','desc','ManufacturedPart','Active','26012024','1','2024-01-26 00:00:00',1,'2024-01-26 08:05:57','2024-01-26 08:05:57','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(27,'26012024-2','desc','BOF','Active',NULL,'1','2024-01-26 00:00:00',1,'2024-01-26 08:29:37','2024-01-26 08:29:37','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(28,'25012026-2','desc','ManufacturedPart','Active','25012024','1','2024-01-26 00:00:00',1,'2024-01-26 17:47:27','2024-01-26 17:47:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(29,'1000987-s','desc','ManufacturedPart','Active',NULL,'1','2024-01-26 00:00:00',1,'2024-01-26 18:01:14','2024-01-26 18:01:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(30,'1000987-V','desc','ManufacturedPart','Active',NULL,'1','2024-01-26 00:00:00',1,'2024-01-26 18:26:39','2024-01-26 18:26:39','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(31,'10009876-XV','e3wd','ManufacturedPart','Active','1000987-V','2','0001-01-01 00:00:00',1,'2024-01-26 18:31:13','2024-01-26 18:31:13','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(32,'26012024-3','desc','RawMaterial','Active','1000987-V','2','2024-01-27 00:00:00',1,'2024-01-26 18:38:37','2024-01-26 18:38:37','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(33,'1000987-AMN','desc','ManufacturedPart','Active',NULL,'1','2024-01-27 00:00:00',1,'2024-01-26 18:39:08','2024-01-26 18:39:08','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(34,'26012024-4','Desc','RawMaterial','Active','1000987-AMN','2','2024-01-27 00:00:00',1,'2024-01-26 18:46:52','2024-01-26 18:46:52','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(35,'1000987-AMO','desc','ManufacturedPart','Active','1000987-AMN','1','2024-01-27 00:00:00',1,'2024-01-26 18:47:39','2024-01-26 18:47:39','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(36,'1000987-xiv','Desc','ManufacturedPart','Active','1000987-s','1','2024-01-27 00:00:00',1,'2024-01-26 18:55:40','2024-01-26 18:55:40','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(37,'1000987-HNF','desc','RawMaterial','Active','1000987-AMN','1','2024-01-27 00:00:00',1,'2024-01-26 18:59:06','2024-01-26 18:59:06','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(38,'1000987-HHJ','Desc','ManufacturedPart','Active','1000987-AMO','1','2024-01-27 00:00:00',1,'2024-01-26 19:00:08','2024-01-26 19:00:08','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(39,'1000987-HHV','desc','ManufacturedPart','Active','1000987-HHJ','1','2024-01-27 00:00:00',1,'2024-01-26 23:12:49','2024-01-26 23:12:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `MasterParts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `OperationLists`
--

DROP TABLE IF EXISTS `OperationLists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `OperationLists` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Operation` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsMultiplePartsOfBOMUsed` tinyint(1) NOT NULL DEFAULT '0',
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `OperationList_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OperationLists`
--

LOCK TABLES `OperationLists` WRITE;
/*!40000 ALTER TABLE `OperationLists` DISABLE KEYS */;
INSERT INTO `OperationLists` VALUES (1,'Machining',0,1,'2023-12-29 07:22:09','2023-12-29 07:22:09','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,'Drilling',0,1,'2023-12-29 12:59:42','2023-12-29 13:01:37','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,'Turning',0,1,'2023-12-29 12:59:53','2023-12-29 12:59:53','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,'Casting',0,1,'2023-12-29 13:00:09','2023-12-29 13:00:09','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,'Forging',0,1,'2023-12-29 13:00:20','2023-12-29 13:00:20','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,'Grinding',0,1,'2023-12-29 13:01:17','2023-12-29 13:01:17','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,'Die Casting',0,1,'2023-12-29 13:03:33','2023-12-29 13:03:33','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(8,'Heat Treatment',0,1,'2023-12-29 13:03:44','2023-12-29 13:03:44','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `OperationLists` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `OperationalDocuments`
--

DROP TABLE IF EXISTS `OperationalDocuments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `OperationalDocuments` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `DocumentTypeId` bigint NOT NULL,
  `IsMandatory` tinyint(1) NOT NULL DEFAULT '0',
  `TenantId` bigint NOT NULL,
  `OperationListId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `OperationalDocument_DocumentTypeId` (`DocumentTypeId`),
  KEY `OperationalDocument_OperationListId` (`OperationListId`),
  KEY `OperationalDocument_TenantId` (`TenantId`),
  CONSTRAINT `FK_OperationalDocuments_OperationLists_OperationListId` FOREIGN KEY (`OperationListId`) REFERENCES `OperationLists` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OperationalDocuments`
--

LOCK TABLES `OperationalDocuments` WRITE;
/*!40000 ALTER TABLE `OperationalDocuments` DISABLE KEYS */;
/*!40000 ALTER TABLE `OperationalDocuments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PartPurchaseDetails`
--

DROP TABLE IF EXISTS `PartPurchaseDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PartPurchaseDetails` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PartId` bigint NOT NULL,
  `SupplierId` bigint NOT NULL,
  `Supplier` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SupplierPartNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LeadTimeInDays` int NOT NULL,
  `MinimumOrderQuantity` int NOT NULL,
  `Price` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ShareOfBusiness` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AdditionalInfo` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `BOFId` bigint DEFAULT '-1',
  `RMId` bigint DEFAULT '-1',
  `MasterPartType` varchar(25) NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `IX_PartPurchaseDetails_SupplierId` (`SupplierId`),
  KEY `PartPurchaseDetail_MasterPartId` (`PartId`),
  KEY `PartPurchaseDetail_TenantId` (`TenantId`),
  CONSTRAINT `PartPurchaseDetails_ibfk_1` FOREIGN KEY (`PartId`) REFERENCES `MasterParts` (`Id`),
  CONSTRAINT `PartPurchaseDetails_ibfk_2` FOREIGN KEY (`SupplierId`) REFERENCES `Companies` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PartPurchaseDetails`
--

LOCK TABLES `PartPurchaseDetails` WRITE;
/*!40000 ALTER TABLE `PartPurchaseDetails` DISABLE KEYS */;
INSERT INTO `PartPurchaseDetails` VALUES (1,3,128,'Customer_001','645454',1,1,'1','1','2',-1,1,'3',1,'2023-12-10 02:03:32','2023-12-10 02:03:32','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,3,129,'Customer_002','tyrdy23',1,1,'2','1','1',-1,1,'3',1,'2023-12-10 02:03:49','2023-12-10 02:03:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,4,128,'Customer_001','tst124',1,1,'2','1',NULL,1,-1,'2',1,'2023-12-10 02:05:27','2023-12-10 02:05:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,4,129,'Customer_002','test1245',1,1,'1','1',NULL,1,-1,'2',1,'2023-12-10 02:05:43','2023-12-10 02:05:43','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,4,130,'Customer_003','56343',2,2,'1','1',NULL,1,-1,'2',1,'2023-12-10 02:07:14','2023-12-10 02:07:23','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,3,130,'Customer_003','test3343',2,2,'1','1',NULL,-1,1,'3',1,'2023-12-10 02:08:09','2023-12-10 02:08:15','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(7,5,130,'Customer_003','1',2,2,'2','1','1',1,-1,'2',1,'2023-12-11 13:41:45','2023-12-11 13:42:03','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(8,9,128,'Customer_001','Test123',1,1,'1','1',NULL,-1,2,'3',1,'2023-12-11 14:08:11','2023-12-11 14:08:11','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(9,18,128,'Customer_001','1',1,1,'1','1',NULL,6,-1,'2',1,'2023-12-16 01:53:02','2023-12-16 01:53:02','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(10,18,129,'Customer_002','2',2,2,'2','2',NULL,6,-1,'2',1,'2023-12-16 01:53:14','2023-12-16 01:53:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(11,20,128,'Customer_001','Test1',10,10,'100','10','Information',7,-1,'2',1,'2023-12-17 22:51:32','2023-12-17 22:51:32','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(12,18,130,'Customer_003','2',2,2,'2','2','1',6,-1,'2',1,'2024-01-25 16:49:44','2024-01-25 16:49:44','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(13,27,128,'Customer_001','1234321',3,3,'3','10','1',16,-1,'2',1,'2024-01-26 08:29:58','2024-01-26 08:30:29','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(14,27,131,'Customer_004','12345431',2,2,'2','10','1',16,-1,'2',1,'2024-01-26 08:30:17','2024-01-26 08:30:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(15,34,128,'Customer_001','3232',2,1,'1','10','1',-1,11,'3',1,'2024-01-26 18:47:17','2024-01-26 18:47:17','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(16,37,128,'Customer_001','100987',1,1,'1','1','1',-1,12,'3',1,'2024-01-26 18:59:36','2024-01-26 18:59:36','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(17,5,130,'Customer_003','1009876',2,2,'2','2','2',1,-1,'2',1,'2024-01-27 06:08:43','2024-01-27 06:08:43','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(18,5,128,'Customer_001','45643',2,2,'2','2','2',1,-1,'2',1,'2024-01-27 06:09:04','2024-01-27 06:09:04','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `PartPurchaseDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PartSpecificMRItems`
--

DROP TABLE IF EXISTS `PartSpecificMRItems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PartSpecificMRItems` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` bigint NOT NULL,
  `ManufacturingResourceId` bigint NOT NULL,
  `PartId` bigint NOT NULL,
  `Notes` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `PartSpecificMRItem_ManufacturingResourceId` (`ManufacturingResourceId`),
  KEY `PartSpecificMRItem_TenantId` (`TenantId`),
  CONSTRAINT `FK_PartSpecificMRItems_ManufacturingResources_ManufacturingReso~` FOREIGN KEY (`ManufacturingResourceId`) REFERENCES `ManufacturingResources` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PartSpecificMRItems`
--

LOCK TABLES `PartSpecificMRItems` WRITE;
/*!40000 ALTER TABLE `PartSpecificMRItems` DISABLE KEYS */;
/*!40000 ALTER TABLE `PartSpecificMRItems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RawMaterialDetails`
--

DROP TABLE IF EXISTS `RawMaterialDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RawMaterialDetails` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PartId` bigint NOT NULL,
  `SupplierId` bigint NOT NULL,
  `RawMaterialMadeType` bigint NOT NULL,
  `RawMaterialMadeSubType` bigint NOT NULL,
  `RawMaterialTypeId` bigint NOT NULL,
  `BaseRawMaterialId` bigint NOT NULL,
  `RawMaterialWeight` varchar(100) NOT NULL,
  `RawMaterialNotes` varchar(450) DEFAULT NULL,
  `Standard` int DEFAULT NULL,
  `MaterialSpecId` int DEFAULT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL,
  `LastModifiedDate` datetime NOT NULL,
  `Creator` varchar(450) NOT NULL,
  `Modifier` varchar(450) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `PartId` (`PartId`),
  KEY `SupplierId` (`SupplierId`),
  CONSTRAINT `RawMaterialDetails_ibfk_1` FOREIGN KEY (`PartId`) REFERENCES `MasterParts` (`Id`),
  CONSTRAINT `RawMaterialDetails_ibfk_2` FOREIGN KEY (`SupplierId`) REFERENCES `Companies` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RawMaterialDetails`
--

LOCK TABLES `RawMaterialDetails` WRITE;
/*!40000 ALTER TABLE `RawMaterialDetails` DISABLE KEYS */;
INSERT INTO `RawMaterialDetails` VALUES (1,6,128,1,2,105,34,'1',NULL,25,21,1,'2023-12-10 02:03:14','2023-12-11 13:37:12','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,9,128,1,1,105,34,'1','1',25,21,1,'2023-12-11 14:07:49','2023-12-11 14:07:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(9,22,128,1,2,103,31,'1','notes',25,20,1,'2023-12-18 00:29:44','2023-12-18 00:29:44','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(10,32,127,1,1,105,34,'100','120',25,21,1,'2024-01-26 18:38:37','2024-01-26 18:38:37','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(11,34,127,1,1,105,34,'12','sdf',25,21,1,'2024-01-26 18:46:52','2024-01-26 18:46:52','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(12,37,131,1,1,105,34,'12','notes',25,21,1,'2024-01-26 18:59:06','2024-01-26 18:59:06','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `RawMaterialDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RawMaterialSpecs`
--

DROP TABLE IF EXISTS `RawMaterialSpecs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RawMaterialSpecs` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `RawMaterialSpec_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RawMaterialSpecs`
--

LOCK TABLES `RawMaterialSpecs` WRITE;
/*!40000 ALTER TABLE `RawMaterialSpecs` DISABLE KEYS */;
INSERT INTO `RawMaterialSpecs` VALUES (19,'SPEC-1',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(20,'SPEC-2',1,'2023-11-20 12:36:33','2023-11-20 12:36:33','',''),(21,'rod weldiing',0,'2023-11-21 06:06:27','2023-11-21 06:06:27','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `RawMaterialSpecs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RawMaterialStandards`
--

DROP TABLE IF EXISTS `RawMaterialStandards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RawMaterialStandards` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `RawMaterialStandard_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RawMaterialStandards`
--

LOCK TABLES `RawMaterialStandards` WRITE;
/*!40000 ALTER TABLE `RawMaterialStandards` DISABLE KEYS */;
INSERT INTO `RawMaterialStandards` VALUES (25,'SAE',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(26,'ISO',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(27,'DIN',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(28,'JIS',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','','');
/*!40000 ALTER TABLE `RawMaterialStandards` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RawMaterialTypes`
--

DROP TABLE IF EXISTS `RawMaterialTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RawMaterialTypes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `RawMaterialType_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RawMaterialTypes`
--

LOCK TABLES `RawMaterialTypes` WRITE;
/*!40000 ALTER TABLE `RawMaterialTypes` DISABLE KEYS */;
INSERT INTO `RawMaterialTypes` VALUES (101,'Forging',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(102,'Casting',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(103,'Sheet',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(104,'Bar Stock',1,'2023-11-20 12:36:31','2023-11-20 12:36:31','',''),(105,'Welding',0,'2023-11-21 06:03:18','2023-11-21 06:03:18','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `RawMaterialTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Routing`
--

DROP TABLE IF EXISTS `Routing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Routing` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingName` varchar(300) DEFAULT NULL,
  `ManufacturedPartId` bigint NOT NULL,
  `OrigRoutingId` int DEFAULT '0',
  `PreferredRouting` tinyint DEFAULT '0',
  `Status` varchar(50) NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Modifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `ManufacturedPartId` (`ManufacturedPartId`,`RoutingName`),
  KEY `IX_Routing_ManufacturedPartId` (`ManufacturedPartId`),
  CONSTRAINT `FK_Routing_ManufacturedPartNoDetails_ManufacturedPartId` FOREIGN KEY (`ManufacturedPartId`) REFERENCES `ManufacturedPartNoDetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Routing`
--

LOCK TABLES `Routing` WRITE;
/*!40000 ALTER TABLE `Routing` DISABLE KEYS */;
INSERT INTO `Routing` VALUES (57,'Route-z',1,0,0,'Active',0,'2024-01-27 14:51:27.541735','2024-02-12 12:07:40.049739','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(58,'Route-2',1,57,1,'Active',0,'2024-01-27 14:59:11.146408','2024-02-12 12:07:40.049755','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(59,'Route-1',3,0,1,'Active',0,'2024-01-27 19:39:12.301886','2024-01-27 19:41:47.144282','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(60,'Route-2',3,59,0,'Active',0,'2024-01-27 19:41:30.363654','2024-01-27 19:41:47.144354','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(61,'Route-1',11,0,0,'Active',0,'2024-01-27 19:59:43.080737','2024-01-27 19:59:43.080856','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(62,'Route-2',11,61,0,'Active',0,'2024-01-27 20:00:18.234506','2024-01-27 20:00:18.234514','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(63,'Route-1',12,0,0,'Active',0,'2024-01-27 20:11:46.787038','2024-01-27 20:11:46.787207','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(64,'Route-3',1,58,0,'Active',0,'2024-01-28 05:00:37.971896','2024-02-12 12:07:40.049763','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(65,'Route-1',13,0,1,'Active',0,'2024-02-02 02:55:45.098735','2024-02-02 02:56:29.220532','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(66,'Route-2',13,0,0,'Active',0,'2024-02-02 02:56:12.786603','2024-02-02 02:56:29.220546','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(67,'Route-1',14,0,0,'Active',0,'2024-02-04 08:38:11.787209','2024-02-04 08:38:11.787324','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(69,'Route-1',2,0,1,'Active',0,'2024-02-09 08:32:05.883502','2024-02-09 08:55:00.075039','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(70,'Route-`P',1,64,0,'Active',0,'2024-02-12 11:58:51.223628','2024-02-12 12:07:40.049771','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `Routing` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RoutingStep`
--

DROP TABLE IF EXISTS `RoutingStep`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RoutingStep` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingId` bigint NOT NULL,
  `StepNumber` varchar(255) NOT NULL,
  `StepDescription` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `RoutingStepOperation` varchar(30) NOT NULL,
  `RoutingStepLocation` varchar(30) NOT NULL,
  `RoutingStepSequence` int NOT NULL,
  `NumberOfSimMachines` int DEFAULT '1',
  `Status` varchar(50) NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Modifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoutingId` (`RoutingId`,`StepNumber`),
  KEY `IX_RoutingStep_RoutingId` (`RoutingId`),
  CONSTRAINT `FK_RoutingStep_Routing_RoutingId` FOREIGN KEY (`RoutingId`) REFERENCES `Routing` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=102 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RoutingStep`
--

LOCK TABLES `RoutingStep` WRITE;
/*!40000 ALTER TABLE `RoutingStep` DISABLE KEYS */;
INSERT INTO `RoutingStep` VALUES (92,58,'Step-1','desc','1','1',1,3,'Active',1,'2024-01-27 15:05:00.613138','2024-01-27 15:05:00.613244','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(93,57,'Step-1','Desc','1','1',1,2,'Active',1,'2024-01-27 19:27:37.861998','2024-01-27 19:27:37.862113','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(94,59,'Step-1','descf','2','2',1,2,'Active',1,'2024-01-27 19:40:07.379976','2024-01-27 19:40:07.379986','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(95,60,'Step-1','desc','1','1',1,2,'Active',1,'2024-01-27 19:42:39.710399','2024-01-27 19:42:39.710404','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(96,63,'Step-1','desc','1','3',1,0,'Active',1,'2024-01-27 20:12:17.370825','2024-01-27 20:12:17.370830','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(97,58,'Step-2','test','3','2',1,1,'Active',1,'2024-01-28 03:48:53.709483','2024-01-28 03:48:53.709646','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(98,57,'Step-2','desc','1','2',1,1,'Active',1,'2024-01-28 05:15:56.652088','2024-01-28 05:15:56.652090','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(99,69,'Step-1','test','1','1',1,3,'Active',1,'2024-02-09 08:32:37.365209','2024-02-09 08:32:37.365214','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(101,64,'Step-1','test','1','1',1,3,'Active',1,'2024-02-12 12:12:43.877930','2024-02-12 12:12:43.877937','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `RoutingStep` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RoutingStepMachine`
--

DROP TABLE IF EXISTS `RoutingStepMachine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RoutingStepMachine` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` bigint NOT NULL,
  `MachineId` bigint NOT NULL,
  `RoutingStepId` bigint NOT NULL,
  `SetupTime` varchar(10) NOT NULL,
  `FloorToFloorTime` varchar(10) NOT NULL,
  `FirstPieceProcessingTime` varchar(10) NOT NULL,
  `NoOfPartsPerLoading` int NOT NULL,
  `PreferredMachine` tinyint DEFAULT '0',
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Modifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_RoutingStepMachine_MachineId` (`MachineId`),
  KEY `IX_RoutingStepMachine_RoutingStepId` (`RoutingStepId`),
  CONSTRAINT `FK_RoutingStepMachine_Machines_MachineId` FOREIGN KEY (`MachineId`) REFERENCES `Machines` (`Id`),
  CONSTRAINT `FK_RoutingStepMachine_RoutingStep_RoutingStepId` FOREIGN KEY (`RoutingStepId`) REFERENCES `RoutingStep` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RoutingStepMachine`
--

LOCK TABLES `RoutingStepMachine` WRITE;
/*!40000 ALTER TABLE `RoutingStepMachine` DISABLE KEYS */;
INSERT INTO `RoutingStepMachine` VALUES (70,1,1,92,'09:00:00','10:00:00','11:00:00',100,1,'2024-01-27 15:05:32.664634','2024-02-12 11:49:01.812696','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(71,1,3,92,'09:00:00','10:00:00','11:30:00',100,1,'2024-01-27 15:06:18.349564','2024-02-12 11:49:01.812756','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(72,1,1,93,'09:00:00','10:00:00','11:00:00',10,0,'2024-01-27 19:28:12.070307','2024-01-27 19:28:12.070312','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(73,1,3,93,'09:00:00','10:00:00','11:00:00',100,0,'2024-01-27 19:28:36.545827','2024-01-27 19:28:36.545831','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(74,1,1,95,'10:00:00','10:00:00','10:00:00',1,0,'2024-01-27 19:43:02.224041','2024-02-10 09:49:26.102851','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(75,1,2,95,'10:00:00','10:00:00','10:00:00',1,0,'2024-01-27 19:43:11.034055','2024-02-10 09:49:26.103039','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(76,1,2,92,'10:00:00','10:00:00','10:00:00',1,1,'2024-01-28 03:34:18.432228','2024-02-12 11:49:01.812927','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(77,1,2,92,'09:00:00','10:00:00','12:00:00',100,1,'2024-01-28 04:36:57.576720','2024-02-12 11:49:01.812993','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(78,1,3,92,'09:00:00','10:00:00','12:00:00',100,0,'2024-01-28 04:38:27.012548','2024-02-12 11:49:01.813046','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(79,1,1,97,'10:00:00','10:00:00','10:00:00',1,0,'2024-02-06 13:03:01.516852','2024-02-06 13:03:09.024756','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(80,1,1,99,'10:00:00','10:00:00','10:00:00',1,0,'2024-02-09 08:32:51.904091','2024-02-09 08:32:51.904093','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(81,1,3,92,'09:00:00','10:00:00','12:00:00',100,0,'2024-02-10 10:47:44.280936','2024-02-12 11:49:01.813093','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(82,1,2,101,'10:00:00','10:00:00','10:00:00',1,1,'2024-02-12 12:13:00.933981','2024-02-12 12:13:00.933984','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(83,1,2,101,'10:00:00','10:00:00','10:00:00',1,1,'2024-02-12 12:14:51.739145','2024-02-12 12:14:51.739147','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `RoutingStepMachine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RoutingStepPart`
--

DROP TABLE IF EXISTS `RoutingStepPart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RoutingStepPart` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingStepId` bigint NOT NULL,
  `ManufacturedPartId` bigint NOT NULL,
  `BOMId` bigint NOT NULL,
  `QuantityAssembly` int NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Modifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_RoutingStepPart_ManufacturedPartBOMId` (`ManufacturedPartId`),
  KEY `IX_RoutingStepPart_RoutingStepId` (`RoutingStepId`),
  KEY `FK_RoutingStepPart_ManufacturedPartBOMs_BOMId` (`BOMId`),
  CONSTRAINT `FK_RoutingStepPart_ManufacturedPartBOMs_BOMId` FOREIGN KEY (`BOMId`) REFERENCES `MPBOMs` (`Id`),
  CONSTRAINT `FK_RoutingStepPart_ManufacturedPartBOMs_ManufacturedPartId` FOREIGN KEY (`ManufacturedPartId`) REFERENCES `ManufacturedPartNoDetails` (`Id`),
  CONSTRAINT `FK_RoutingStepPart_RoutingStep_RoutingStepId` FOREIGN KEY (`RoutingStepId`) REFERENCES `RoutingStep` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RoutingStepPart`
--

LOCK TABLES `RoutingStepPart` WRITE;
/*!40000 ALTER TABLE `RoutingStepPart` DISABLE KEYS */;
/*!40000 ALTER TABLE `RoutingStepPart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RoutingStepSupplier`
--

DROP TABLE IF EXISTS `RoutingStepSupplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RoutingStepSupplier` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `TenantId` bigint NOT NULL,
  `SupplierId` bigint NOT NULL,
  `RoutingStepId` bigint NOT NULL,
  `OutSourceDays` int NOT NULL,
  `ShareOfBusiness` int NOT NULL,
  `Notes` varchar(255) DEFAULT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Modifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_RoutingStepSupplier_SupplierId` (`SupplierId`),
  KEY `IX_RoutingStepSupplier_RoutingStepId` (`RoutingStepId`),
  CONSTRAINT `FK_RoutingStepSupplier_Companies_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Companies` (`Id`),
  CONSTRAINT `FK_RoutingStepSupplier_RoutingStep_RoutingStepId` FOREIGN KEY (`RoutingStepId`) REFERENCES `RoutingStep` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RoutingStepSupplier`
--

LOCK TABLES `RoutingStepSupplier` WRITE;
/*!40000 ALTER TABLE `RoutingStepSupplier` DISABLE KEYS */;
INSERT INTO `RoutingStepSupplier` VALUES (41,1,128,94,20,20,'notes','2024-01-27 19:40:29.994102','2024-01-27 19:40:29.994104','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(42,1,129,94,20,20,'n','2024-01-27 19:40:43.008459','2024-01-27 19:40:43.008469','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(43,1,130,94,20,20,'Nl','2024-01-27 19:40:58.605955','2024-01-27 19:40:58.605968','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(44,1,128,97,20,20,'n','2024-01-28 03:49:07.362993','2024-01-28 03:49:07.363001','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(45,1,129,97,20,100,'notes','2024-01-28 04:00:23.055098','2024-01-28 04:00:23.055286','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(46,1,132,97,20,100,'Test','2024-01-28 04:00:42.151715','2024-01-28 04:00:42.151721','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(47,1,128,97,20,10,'f','2024-01-28 04:01:36.633423','2024-01-28 04:01:36.633425','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(48,1,128,97,20,20,'b','2024-01-28 04:04:17.993231','2024-01-28 04:04:17.993408','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(49,1,128,97,20,20,'b','2024-01-28 04:04:52.822585','2024-01-28 04:04:52.822591','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(50,1,128,97,20,20,'n','2024-01-28 04:09:48.530764','2024-01-28 04:09:48.531114','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(51,1,129,97,20,20,'i','2024-01-28 04:10:44.783941','2024-01-28 04:10:44.783951','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(52,1,128,97,20,20,'h','2024-01-28 04:14:08.396939','2024-01-28 04:14:08.397150','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(53,1,128,97,20,20,'20','2024-01-28 04:14:58.490646','2024-01-28 04:14:58.490736','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(54,1,128,97,20,20,'n','2024-01-28 04:23:29.645565','2024-01-28 04:23:29.645890','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(55,1,128,98,20,20,'notes','2024-02-02 01:26:10.609171','2024-02-02 01:26:10.609394','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(56,1,128,97,20,20,'n','2024-02-12 10:48:40.858610','2024-02-12 10:48:40.858961','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(57,1,128,101,20,20,'test','2024-02-12 12:17:29.568079','2024-02-12 12:17:29.568082','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `RoutingStepSupplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SubConDetails`
--

DROP TABLE IF EXISTS `SubConDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SubConDetails` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingStepId` bigint NOT NULL,
  `SupplierId` bigint NOT NULL,
  `WorkDone` varchar(4000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TransportTime` int NOT NULL,
  `CostPerPart` decimal(10,2) NOT NULL,
  `Notes` varchar(255) DEFAULT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `IX_SubConDetails_RoutingStepId` (`RoutingStepId`),
  KEY `FK_SubConDetails_Companies_SupplierId` (`SupplierId`),
  CONSTRAINT `FK_SubConDetails_Companies_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Companies` (`Id`),
  CONSTRAINT `FK_SubConDetails_RoutingStep_RoutingStepId` FOREIGN KEY (`RoutingStepId`) REFERENCES `RoutingStep` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SubConDetails`
--

LOCK TABLES `SubConDetails` WRITE;
/*!40000 ALTER TABLE `SubConDetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `SubConDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SubConWorkStepDetails`
--

DROP TABLE IF EXISTS `SubConWorkStepDetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SubConWorkStepDetails` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingStepId` bigint NOT NULL,
  `SubConDetailsId` bigint NOT NULL,
  `WorkStepDesc` varchar(4000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MachineType` int NOT NULL,
  `SetupTime` varchar(10) NOT NULL,
  `FloorToFloorTime` varchar(10) NOT NULL,
  `NoOfPartsPerLoading` int NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `IX_SubConWorkStepDetails_RoutingStepId` (`RoutingStepId`),
  KEY `FK_SubConWorkStepDetails_SubConDetails_SubConDetailsId` (`SubConDetailsId`),
  CONSTRAINT `FK_SubConWorkStepDetails_RoutingStep_RoutingStepId` FOREIGN KEY (`RoutingStepId`) REFERENCES `RoutingStep` (`Id`),
  CONSTRAINT `FK_SubConWorkStepDetails_SubConDetails_SubConDetailsId` FOREIGN KEY (`SubConDetailsId`) REFERENCES `SubConDetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SubConWorkStepDetails`
--

LOCK TABLES `SubConWorkStepDetails` WRITE;
/*!40000 ALTER TABLE `SubConWorkStepDetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `SubConWorkStepDetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UOMs`
--

DROP TABLE IF EXISTS `UOMs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `UOMs` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `UOM_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UOMs`
--

LOCK TABLES `UOMs` WRITE;
/*!40000 ALTER TABLE `UOMs` DISABLE KEYS */;
INSERT INTO `UOMs` VALUES (1,'Numbers',1,'2023-10-12 03:43:34','2023-10-12 03:43:34','',''),(2,'Kgs',1,'2023-10-12 03:43:41','2023-10-12 03:43:41','',''),(3,'Ltrs',1,'2023-10-12 03:43:46','2023-10-12 03:43:46','','');
/*!40000 ALTER TABLE `UOMs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cwbmaster'
--

--
-- Dumping routines for database 'cwbmaster'
--

--
-- Current Database: `cwbidentity`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cwbidentity` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `cwbidentity`;

--
-- Table structure for table `CwbRoleClaims`
--

DROP TABLE IF EXISTS `CwbRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_CwbRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_CwbRoleClaims_CwbRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `CwbRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbRoleClaims`
--

LOCK TABLES `CwbRoleClaims` WRITE;
/*!40000 ALTER TABLE `CwbRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `CwbRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CwbRoles`
--

DROP TABLE IF EXISTS `CwbRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbRoles`
--

LOCK TABLES `CwbRoles` WRITE;
/*!40000 ALTER TABLE `CwbRoles` DISABLE KEYS */;
INSERT INTO `CwbRoles` VALUES ('95be3795-ba3d-41b0-b7f9-efecfcb9fe4c','Admin','ADMIN','964ae382-88a5-4b53-90f0-542d72839f11'),('fb68a5a9-1e40-42f1-9ea9-df2b0ab38850','Super Admin','SUPER ADMIN','1676746b-401d-47a6-9f16-312635ed5bf4');
/*!40000 ALTER TABLE `CwbRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CwbUserClaims`
--

DROP TABLE IF EXISTS `CwbUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_CwbUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_CwbUserClaims_CwbUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `CwbUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbUserClaims`
--

LOCK TABLES `CwbUserClaims` WRITE;
/*!40000 ALTER TABLE `CwbUserClaims` DISABLE KEYS */;
INSERT INTO `CwbUserClaims` VALUES (1,'fb934ee3-54bd-4439-930c-f5860894d742','name','Super Admin'),(2,'fb934ee3-54bd-4439-930c-f5860894d742','given_name','Super'),(3,'fb934ee3-54bd-4439-930c-f5860894d742','family_name','Admin'),(4,'fb934ee3-54bd-4439-930c-f5860894d742','role','Super Admin'),(5,'fb934ee3-54bd-4439-930c-f5860894d742','TenantId','0'),(6,'50e73e03-3e82-41c7-8eb1-0c53880ed0e6','TenantId','1'),(7,'50e73e03-3e82-41c7-8eb1-0c53880ed0e6','role','Admin'),(8,'50e73e03-3e82-41c7-8eb1-0c53880ed0e6','family_name','Admin'),(9,'50e73e03-3e82-41c7-8eb1-0c53880ed0e6','given_name','Kgk1'),(10,'50e73e03-3e82-41c7-8eb1-0c53880ed0e6','name','Kgk1 Admin'),(11,'4ca6d3bf-2757-46dd-ad28-f23e297746f2','TenantId','2'),(12,'4ca6d3bf-2757-46dd-ad28-f23e297746f2','role','Admin'),(13,'4ca6d3bf-2757-46dd-ad28-f23e297746f2','family_name','Admin'),(14,'4ca6d3bf-2757-46dd-ad28-f23e297746f2','given_name','Kgk2'),(15,'4ca6d3bf-2757-46dd-ad28-f23e297746f2','name','Kgk2 Admin');
/*!40000 ALTER TABLE `CwbUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CwbUserLogins`
--

DROP TABLE IF EXISTS `CwbUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbUserLogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_CwbUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_CwbUserLogins_CwbUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `CwbUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbUserLogins`
--

LOCK TABLES `CwbUserLogins` WRITE;
/*!40000 ALTER TABLE `CwbUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `CwbUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CwbUserRoles`
--

DROP TABLE IF EXISTS `CwbUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_CwbUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_CwbUserRoles_CwbRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `CwbRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CwbUserRoles_CwbUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `CwbUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbUserRoles`
--

LOCK TABLES `CwbUserRoles` WRITE;
/*!40000 ALTER TABLE `CwbUserRoles` DISABLE KEYS */;
INSERT INTO `CwbUserRoles` VALUES ('4ca6d3bf-2757-46dd-ad28-f23e297746f2','95be3795-ba3d-41b0-b7f9-efecfcb9fe4c'),('50e73e03-3e82-41c7-8eb1-0c53880ed0e6','95be3795-ba3d-41b0-b7f9-efecfcb9fe4c'),('fb934ee3-54bd-4439-930c-f5860894d742','fb68a5a9-1e40-42f1-9ea9-df2b0ab38850');
/*!40000 ALTER TABLE `CwbUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CwbUserToken`
--

DROP TABLE IF EXISTS `CwbUserToken`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbUserToken` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_CwbUserToken_CwbUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `CwbUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbUserToken`
--

LOCK TABLES `CwbUserToken` WRITE;
/*!40000 ALTER TABLE `CwbUserToken` DISABLE KEYS */;
/*!40000 ALTER TABLE `CwbUserToken` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CwbUsers`
--

DROP TABLE IF EXISTS `CwbUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CwbUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TenantId` int NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CwbUsers`
--

LOCK TABLES `CwbUsers` WRITE;
/*!40000 ALTER TABLE `CwbUsers` DISABLE KEYS */;
INSERT INTO `CwbUsers` VALUES ('4ca6d3bf-2757-46dd-ad28-f23e297746f2','Kgk2','Admin',0,'kgk2admin','KGK2ADMIN','admin2@kgk.com','ADMIN2@KGK.COM',1,'AQAAAAEAACcQAAAAEMm40dNsDW17Voe0Ty/60qArfPgFaDQ2f4kTyCY7ucG1YTu9w2wrMLT6xhHRzoqPww==','E2NAFBPCLRMZIHVMCH3742FYKORQBGGQ','88c3919d-1481-40f4-a5c9-e41eeacc7a49','0000000000',1,0,NULL,1,0),('50e73e03-3e82-41c7-8eb1-0c53880ed0e6','Kgk1','Admin',0,'kgk1admin','KGK1ADMIN','admin1@kgk.com','ADMIN1@KGK.COM',1,'AQAAAAEAACcQAAAAEF/+TpwPn1qewmwfQGXHxP5P6K9zLhchig0yCdMQLaBKdjEF7AabhvKwZgfJvwDp0A==','NFXYWXSL54OAN573BMOBEUM5I452F6DQ','fb0b6396-009a-44e8-b6db-cddde09f0542','0000000000',1,0,NULL,1,0),('fb934ee3-54bd-4439-930c-f5860894d742','Super','Admin',0,'superadmin','SUPERADMIN','superadmin@cwb.com','SUPERADMIN@CWB.COM',1,'AQAAAAEAACcQAAAAEF9Is4zmo6cJAExQBQajzG9tJUA6+dblPhk0iOQGoP4O5Z6eNihM7e1CQYx+bAHAsw==','OYW6TV2QKUAVVUP4B3KE5GYUFCFLBCKH','63c24dd5-ff50-4be5-937c-a193fd6096ea','0000000000',1,0,NULL,1,0);
/*!40000 ALTER TABLE `CwbUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cwbidentity'
--

--
-- Dumping routines for database 'cwbidentity'
--

--
-- Current Database: `cwbtenant`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cwbtenant` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `cwbtenant`;

--
-- Table structure for table `TenantRequests`
--

DROP TABLE IF EXISTS `TenantRequests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TenantRequests` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CompanyInfo` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `RequestStatus` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Comments` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TenantRequests`
--

LOCK TABLES `TenantRequests` WRITE;
/*!40000 ALTER TABLE `TenantRequests` DISABLE KEYS */;
/*!40000 ALTER TABLE `TenantRequests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tenants`
--

DROP TABLE IF EXISTS `Tenants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Tenants` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CompanyInfo` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `TenantCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsActive` tinyint(1) NOT NULL DEFAULT '0',
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tenants`
--

LOCK TABLES `Tenants` WRITE;
/*!40000 ALTER TABLE `Tenants` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tenants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cwbtenant'
--

--
-- Dumping routines for database 'cwbtenant'
--

--
-- Current Database: `cwbcompanysettings`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cwbcompanysettings` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `cwbcompanysettings`;

--
-- Table structure for table `Designations`
--

DROP TABLE IF EXISTS `Designations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Designations` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Designation_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Designations`
--

LOCK TABLES `Designations` WRITE;
/*!40000 ALTER TABLE `Designations` DISABLE KEYS */;
/*!40000 ALTER TABLE `Designations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `DocumentTypes`
--

DROP TABLE IF EXISTS `DocumentTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `DocumentTypes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Extension` varchar(30) NOT NULL,
  `TenantId` bigint NOT NULL,
  `IsUploadedByUser` tinyint(1) NOT NULL DEFAULT '0',
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `DocumentType_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DocumentTypes`
--

LOCK TABLES `DocumentTypes` WRITE;
/*!40000 ALTER TABLE `DocumentTypes` DISABLE KEYS */;
INSERT INTO `DocumentTypes` VALUES (1,'test','test','te',1,0,'2024-01-02 14:33:35','2024-01-02 14:33:35','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,'te','te','re',1,0,'2024-01-02 14:40:41','2024-01-02 14:40:41','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,'Drawing','drawing','DRW',1,0,'2024-01-03 06:30:49','2024-01-03 06:30:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,'PDF','PDF','pdf',1,0,'2024-01-03 06:31:03','2024-01-03 06:31:03','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,'Text','text','txt',1,0,'2024-01-03 06:31:14','2024-01-03 06:31:14','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `DocumentTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Plants`
--

DROP TABLE IF EXISTS `Plants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Plants` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `IsMainPlant` tinyint(1) NOT NULL DEFAULT '0',
  `IsProductDesigned` tinyint(1) NOT NULL DEFAULT '0',
  `Address` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Notes` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Plant_TenantId` (`TenantId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Plants`
--

LOCK TABLES `Plants` WRITE;
/*!40000 ALTER TABLE `Plants` DISABLE KEYS */;
INSERT INTO `Plants` VALUES (1,'test',1,1,1,'test','test','2023-12-29 01:53:03','2023-12-29 01:53:03','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(2,'test1',1,1,0,'test1','test','2023-12-29 02:55:04','2023-12-29 02:55:04','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(3,'test2',1,1,0,'test2','test','2023-12-29 02:55:19','2023-12-29 02:55:19','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(4,'test3',1,1,1,'test3','test3','2023-12-29 06:55:21','2023-12-29 06:55:21','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(5,'Plant-01',1,1,0,'Pune','Notes....','2023-12-30 00:27:45','2023-12-30 00:27:45','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(6,'testplan',1,1,1,'testloc','notes...','2024-01-03 06:24:03','2024-01-03 06:24:03','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `Plants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Sections`
--

DROP TABLE IF EXISTS `Sections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Sections` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `ParentSectionId` bigint NOT NULL,
  `ShopDepartmentId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Section_ShopDepartmentId` (`ShopDepartmentId`),
  KEY `Section_TenantId` (`TenantId`),
  CONSTRAINT `FK_Sections_ShopDepartments_ShopDepartmentId` FOREIGN KEY (`ShopDepartmentId`) REFERENCES `ShopDepartments` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Sections`
--

LOCK TABLES `Sections` WRITE;
/*!40000 ALTER TABLE `Sections` DISABLE KEYS */;
/*!40000 ALTER TABLE `Sections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ShopDepartments`
--

DROP TABLE IF EXISTS `ShopDepartments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ShopDepartments` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NoOfShifts` int NOT NULL DEFAULT '1',
  `TenantId` bigint NOT NULL,
  `PlantId` bigint NOT NULL,
  `Activity` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `ShopDepartment_PlantId` (`PlantId`),
  KEY `ShopDepartment_TenantId` (`TenantId`),
  CONSTRAINT `FK_ShopDepartments_Plants_PlantId` FOREIGN KEY (`PlantId`) REFERENCES `Plants` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ShopDepartments`
--

LOCK TABLES `ShopDepartments` WRITE;
/*!40000 ALTER TABLE `ShopDepartments` DISABLE KEYS */;
INSERT INTO `ShopDepartments` VALUES (1,'Production',3,1,1,NULL,'2023-12-29 12:49:37','2023-12-29 12:49:37','',''),(2,'Production',3,1,2,NULL,'2023-12-29 12:50:27','2023-12-29 12:50:27','',''),(3,'Production',3,1,3,NULL,'2023-12-29 12:50:27','2023-12-29 12:50:27','',''),(4,'Production',3,1,4,NULL,'2023-12-29 12:50:27','2023-12-29 12:50:27','',''),(5,'Process Planning',1,1,1,NULL,'2023-12-29 12:51:25','2023-12-29 12:51:25','',''),(6,'Marketing',1,1,1,NULL,'2023-12-29 12:51:27','2023-12-29 12:51:27','',''),(7,'Test',1,1,1,NULL,'2024-01-03 05:46:49','2024-01-03 05:46:49','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6'),(8,'Dept001',1,1,6,NULL,'2024-01-03 06:25:17','2024-01-03 06:25:17','50e73e03-3e82-41c7-8eb1-0c53880ed0e6','50e73e03-3e82-41c7-8eb1-0c53880ed0e6');
/*!40000 ALTER TABLE `ShopDepartments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cwbcompanysettings'
--

--
-- Dumping routines for database 'cwbcompanysettings'
--

--
-- Current Database: `cwbmodule`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cwbmodule` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `cwbmodule`;

--
-- Table structure for table `ModuleTenantConfigs`
--

DROP TABLE IF EXISTS `ModuleTenantConfigs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ModuleTenantConfigs` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `ModuleId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `ModuleTenantConfig_ModuleId` (`ModuleId`),
  CONSTRAINT `FK_ModuleTenantConfigs_Modules_ModuleId` FOREIGN KEY (`ModuleId`) REFERENCES `Modules` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ModuleTenantConfigs`
--

LOCK TABLES `ModuleTenantConfigs` WRITE;
/*!40000 ALTER TABLE `ModuleTenantConfigs` DISABLE KEYS */;
/*!40000 ALTER TABLE `ModuleTenantConfigs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ModuleTypes`
--

DROP TABLE IF EXISTS `ModuleTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ModuleTypes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Type` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ModuleTypes`
--

LOCK TABLES `ModuleTypes` WRITE;
/*!40000 ALTER TABLE `ModuleTypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `ModuleTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Modules`
--

DROP TABLE IF EXISTS `Modules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Modules` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ModuleKey` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '0',
  `ModuleTypeId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Module_ModuleTypeId` (`ModuleTypeId`),
  CONSTRAINT `FK_Modules_ModuleTypes_ModuleTypeId` FOREIGN KEY (`ModuleTypeId`) REFERENCES `ModuleTypes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Modules`
--

LOCK TABLES `Modules` WRITE;
/*!40000 ALTER TABLE `Modules` DISABLE KEYS */;
/*!40000 ALTER TABLE `Modules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cwbmodule'
--

--
-- Dumping routines for database 'cwbmodule'
--

--
-- Current Database: `cwbsimulator`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `cwbsimulator` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `cwbsimulator`;

--
-- Table structure for table `Boms`
--

DROP TABLE IF EXISTS `Boms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Boms` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `ItemMasterId` bigint NOT NULL,
  `Quantity` int NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Bom_ItemMasterId` (`ItemMasterId`),
  KEY `Bom_TenantId` (`TenantId`),
  CONSTRAINT `FK_Boms_ItemMasters_ItemMasterId` FOREIGN KEY (`ItemMasterId`) REFERENCES `ItemMasters` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Boms`
--

LOCK TABLES `Boms` WRITE;
/*!40000 ALTER TABLE `Boms` DISABLE KEYS */;
/*!40000 ALTER TABLE `Boms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Designations`
--

DROP TABLE IF EXISTS `Designations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Designations` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Designation_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Designations`
--

LOCK TABLES `Designations` WRITE;
/*!40000 ALTER TABLE `Designations` DISABLE KEYS */;
/*!40000 ALTER TABLE `Designations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `DocumentTypes`
--

DROP TABLE IF EXISTS `DocumentTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `DocumentTypes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `IsUploadedByUser` tinyint(1) NOT NULL DEFAULT '0',
  `ShopDepartmentId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `DocumentType_ShopDepartmentId` (`ShopDepartmentId`),
  KEY `DocumentType_TenantId` (`TenantId`),
  CONSTRAINT `FK_DocumentTypes_ShopDepartments_ShopDepartmentId` FOREIGN KEY (`ShopDepartmentId`) REFERENCES `ShopDepartments` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DocumentTypes`
--

LOCK TABLES `DocumentTypes` WRITE;
/*!40000 ALTER TABLE `DocumentTypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `DocumentTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ItemMasters`
--

DROP TABLE IF EXISTS `ItemMasters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ItemMasters` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `CustomerId` bigint NOT NULL,
  `PartNo` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RevNo` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RevDate` datetime NOT NULL,
  `PartDescription` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `PartAssembly` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MakeBOF` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `ItemMaster_CustomerId` (`CustomerId`),
  KEY `ItemMaster_TenantId` (`TenantId`),
  CONSTRAINT `FK_ItemMasters_Vendors_CustomerId` FOREIGN KEY (`CustomerId`) REFERENCES `Vendors` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ItemMasters`
--

LOCK TABLES `ItemMasters` WRITE;
/*!40000 ALTER TABLE `ItemMasters` DISABLE KEYS */;
/*!40000 ALTER TABLE `ItemMasters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MRBomGroups`
--

DROP TABLE IF EXISTS `MRBomGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MRBomGroups` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `MRBomGroups_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MRBomGroups`
--

LOCK TABLES `MRBomGroups` WRITE;
/*!40000 ALTER TABLE `MRBomGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `MRBomGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MRBoms`
--

DROP TABLE IF EXISTS `MRBoms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MRBoms` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `ItemType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ItemDescription` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `SupplierId` bigint NOT NULL,
  `UoM` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ConsumptionType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cost` double NOT NULL,
  `QuantityOnHand` bigint NOT NULL,
  `MRBomGroupId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `MRBoms_MRBomGroupId` (`MRBomGroupId`),
  KEY `MRBoms_SupplierId` (`SupplierId`),
  KEY `MRBoms_TenantId` (`TenantId`),
  CONSTRAINT `FK_MRBoms_MRBomGroups_MRBomGroupId` FOREIGN KEY (`MRBomGroupId`) REFERENCES `MRBomGroups` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_MRBoms_Vendors_SupplierId` FOREIGN KEY (`SupplierId`) REFERENCES `Vendors` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MRBoms`
--

LOCK TABLES `MRBoms` WRITE;
/*!40000 ALTER TABLE `MRBoms` DISABLE KEYS */;
/*!40000 ALTER TABLE `MRBoms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MachineTypes`
--

DROP TABLE IF EXISTS `MachineTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MachineTypes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `MachineType_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MachineTypes`
--

LOCK TABLES `MachineTypes` WRITE;
/*!40000 ALTER TABLE `MachineTypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `MachineTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Machines`
--

DROP TABLE IF EXISTS `Machines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Machines` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Manufacturer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SerialNumber` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `MachineTypeId` bigint NOT NULL,
  `PlantId` bigint NOT NULL,
  `ShopDepartmentId` bigint NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Machine_MachineTypeId` (`MachineTypeId`),
  KEY `Machine_PlantId` (`PlantId`),
  KEY `Machine_ShopDepartmentId` (`ShopDepartmentId`),
  KEY `Machine_TenantId` (`TenantId`),
  CONSTRAINT `FK_Machines_MachineTypes_MachineTypeId` FOREIGN KEY (`MachineTypeId`) REFERENCES `MachineTypes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Machines_Plants_PlantId` FOREIGN KEY (`PlantId`) REFERENCES `Plants` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Machines_ShopDepartments_ShopDepartmentId` FOREIGN KEY (`ShopDepartmentId`) REFERENCES `ShopDepartments` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Machines`
--

LOCK TABLES `Machines` WRITE;
/*!40000 ALTER TABLE `Machines` DISABLE KEYS */;
/*!40000 ALTER TABLE `Machines` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Plants`
--

DROP TABLE IF EXISTS `Plants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Plants` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `IsMainPlant` tinyint(1) NOT NULL DEFAULT '0',
  `IsProductDesigned` tinyint(1) NOT NULL DEFAULT '0',
  `Address` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Notes` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Plant_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Plants`
--

LOCK TABLES `Plants` WRITE;
/*!40000 ALTER TABLE `Plants` DISABLE KEYS */;
/*!40000 ALTER TABLE `Plants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Routing`
--

DROP TABLE IF EXISTS `Routing`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Routing` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PartId` bigint NOT NULL,
  `ItemMasterId` bigint DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IsPreferredRoute` tinyint(1) NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastModifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_Routing_ItemMasterId` (`ItemMasterId`),
  CONSTRAINT `FK_Routing_ItemMasters_ItemMasterId` FOREIGN KEY (`ItemMasterId`) REFERENCES `ItemMasters` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Routing`
--

LOCK TABLES `Routing` WRITE;
/*!40000 ALTER TABLE `Routing` DISABLE KEYS */;
/*!40000 ALTER TABLE `Routing` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RoutingStep`
--

DROP TABLE IF EXISTS `RoutingStep`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RoutingStep` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingId` bigint NOT NULL,
  `SequenceNumber` int NOT NULL,
  `StepNumber` int NOT NULL,
  `StepDescription` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `StepLocation` int NOT NULL,
  `OutSourceDays` int NOT NULL,
  `IsCriticalOperations` tinyint(1) NOT NULL,
  `IsCriticalSetup` tinyint(1) NOT NULL,
  `RoutingSequence` int NOT NULL,
  `MachineType` int NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastModifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_RoutingStep_RoutingId` (`RoutingId`),
  CONSTRAINT `FK_RoutingStep_Routing_RoutingId` FOREIGN KEY (`RoutingId`) REFERENCES `Routing` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RoutingStep`
--

LOCK TABLES `RoutingStep` WRITE;
/*!40000 ALTER TABLE `RoutingStep` DISABLE KEYS */;
/*!40000 ALTER TABLE `RoutingStep` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RoutingStepMachineConfig`
--

DROP TABLE IF EXISTS `RoutingStepMachineConfig`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RoutingStepMachineConfig` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `RoutingStepId` bigint NOT NULL,
  `MachineId` bigint NOT NULL,
  `SetupTime` time(6) NOT NULL,
  `FirstPieceProcessTime` time(6) NOT NULL,
  `ResidenceTime` time(6) NOT NULL,
  `FloorToFloorTime` time(6) NOT NULL,
  `NoOfPartsPerLoading` int NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime(6) NOT NULL,
  `LastModifiedDate` datetime(6) NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastModifier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_RoutingStepMachineConfig_MachineId` (`MachineId`),
  KEY `IX_RoutingStepMachineConfig_RoutingStepId` (`RoutingStepId`),
  CONSTRAINT `FK_RoutingStepMachineConfig_Machines_MachineId` FOREIGN KEY (`MachineId`) REFERENCES `Machines` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_RoutingStepMachineConfig_RoutingStep_RoutingStepId` FOREIGN KEY (`RoutingStepId`) REFERENCES `RoutingStep` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RoutingStepMachineConfig`
--

LOCK TABLES `RoutingStepMachineConfig` WRITE;
/*!40000 ALTER TABLE `RoutingStepMachineConfig` DISABLE KEYS */;
/*!40000 ALTER TABLE `RoutingStepMachineConfig` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Sections`
--

DROP TABLE IF EXISTS `Sections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Sections` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `ParentSectionId` bigint NOT NULL,
  `ShopDepartmentId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Section_ShopDepartmentId` (`ShopDepartmentId`),
  KEY `Section_TenantId` (`TenantId`),
  CONSTRAINT `FK_Sections_ShopDepartments_ShopDepartmentId` FOREIGN KEY (`ShopDepartmentId`) REFERENCES `ShopDepartments` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Sections`
--

LOCK TABLES `Sections` WRITE;
/*!40000 ALTER TABLE `Sections` DISABLE KEYS */;
/*!40000 ALTER TABLE `Sections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ShopDepartments`
--

DROP TABLE IF EXISTS `ShopDepartments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ShopDepartments` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NoOfShifts` int NOT NULL DEFAULT '1',
  `TenantId` bigint NOT NULL,
  `PlantId` bigint NOT NULL,
  `Activity` varchar(4000) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `ShopDepartment_PlantId` (`PlantId`),
  KEY `ShopDepartment_TenantId` (`TenantId`),
  CONSTRAINT `FK_ShopDepartments_Plants_PlantId` FOREIGN KEY (`PlantId`) REFERENCES `Plants` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ShopDepartments`
--

LOCK TABLES `ShopDepartments` WRITE;
/*!40000 ALTER TABLE `ShopDepartments` DISABLE KEYS */;
/*!40000 ALTER TABLE `ShopDepartments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Vendors`
--

DROP TABLE IF EXISTS `Vendors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Vendors` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VendorType` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`),
  KEY `Vendor_TenantId` (`TenantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Vendors`
--

LOCK TABLES `Vendors` WRITE;
/*!40000 ALTER TABLE `Vendors` DISABLE KEYS */;
/*!40000 ALTER TABLE `Vendors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `WorkDayMaster`
--

DROP TABLE IF EXISTS `WorkDayMaster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `WorkDayMaster` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `WeeklyOff` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NoOfShifts` int NOT NULL DEFAULT '1',
  `FirstShiftStartTime` time NOT NULL,
  `FirstShiftDuration` time NOT NULL,
  `SecondShiftDuration` time NOT NULL,
  `ThirdShiftDuration` time NOT NULL,
  `TenantId` bigint NOT NULL,
  `CreationDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastModifiedDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Creator` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `Modifier` varchar(450) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `WorkDayMaster`
--

LOCK TABLES `WorkDayMaster` WRITE;
/*!40000 ALTER TABLE `WorkDayMaster` DISABLE KEYS */;
/*!40000 ALTER TABLE `WorkDayMaster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'cwbsimulator'
--

--
-- Dumping routines for database 'cwbsimulator'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-17 12:23:43
