-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: cwbcompanysettings
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
-- Dumping events for database 'cwbcompanysettings'
--

--
-- Dumping routines for database 'cwbcompanysettings'
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
-- Dumping events for database 'cwbsimulator'
--

--
-- Dumping routines for database 'cwbsimulator'
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
-- Dumping events for database 'cwbtenant'
--

--
-- Dumping routines for database 'cwbtenant'
--

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
-- Dumping events for database 'cwbmaster'
--

--
-- Dumping routines for database 'cwbmaster'
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
-- Dumping events for database 'cwbmodule'
--

--
-- Dumping routines for database 'cwbmodule'
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
-- Dumping events for database 'cwbidentity'
--

--
-- Dumping routines for database 'cwbidentity'
--

--
-- Current Database: `db`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `db`;

--
-- Dumping events for database 'db'
--

--
-- Dumping routines for database 'db'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-17 19:56:26
