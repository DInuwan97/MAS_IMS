CREATE DATABASE  IF NOT EXISTS `mas_isscs` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `mas_isscs`;
-- MySQL dump 10.13  Distrib 8.0.17, for Win64 (x86_64)
--
-- Host: localhost    Database: mas_isscs
-- ------------------------------------------------------
-- Server version	8.0.17

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
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comment` (
  `commentID` int(11) NOT NULL AUTO_INCREMENT,
  `comment` varchar(1000) DEFAULT NULL,
  `feedbackId` int(11) NOT NULL,
  `dateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`commentID`),
  KEY `feedbackId_idx` (`feedbackId`),
  CONSTRAINT `feedbackId` FOREIGN KEY (`feedbackId`) REFERENCES `feedback` (`feedbackId`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
INSERT INTO `comment` VALUES (1,'Good Report',1,'2019-08-16 11:04:16'),(2,'Fine Report',1,'2019-08-16 11:04:57'),(3,'Well done on the Report',1,'2019-08-16 11:04:57'),(4,'Good Report',1,'2019-08-21 00:14:07'),(5,'Fine Report',1,'2019-08-21 00:14:08'),(6,'Well done on the Report',1,'2019-08-21 00:14:08'),(7,'Good Report',1,'2019-08-21 00:14:14'),(8,'Fine Report',1,'2019-08-21 00:14:15'),(9,'Well done on the Report',1,'2019-08-21 00:14:15'),(10,'Superb Report',2,'2019-08-21 20:10:41'),(11,'Nice writing',3,'2019-08-21 20:23:14'),(12,'Superb Report',2,'2019-08-26 21:13:55');
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `feedback`
--

DROP TABLE IF EXISTS `feedback`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `feedback` (
  `feedbackId` int(11) NOT NULL AUTO_INCREMENT,
  `userId` int(11) NOT NULL,
  `tokenId` int(11) NOT NULL,
  `rating` int(11) DEFAULT '0',
  PRIMARY KEY (`feedbackId`),
  KEY `tokenId_idx` (`tokenId`),
  CONSTRAINT `tokenId` FOREIGN KEY (`tokenId`) REFERENCES `tokens` (`TokenID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci KEY_BLOCK_SIZE=1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feedback`
--

LOCK TABLES `feedback` WRITE;
/*!40000 ALTER TABLE `feedback` DISABLE KEYS */;
INSERT INTO `feedback` VALUES (1,1,30,1),(2,2,30,1),(3,3,30,1),(4,4,30,1),(5,5,30,1),(6,6,30,2),(7,7,30,2),(8,8,30,2),(9,9,30,1),(10,9,33,1),(11,9,35,2),(12,9,34,1),(13,9,31,0);
/*!40000 ALTER TABLE `feedback` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `images`
--

DROP TABLE IF EXISTS `images`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `images` (
  `imgID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ProblemID` int(11) NOT NULL,
  `ImagePath` text NOT NULL,
  PRIMARY KEY (`imgID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `images`
--

LOCK TABLES `images` WRITE;
/*!40000 ALTER TABLE `images` DISABLE KEYS */;
INSERT INTO `images` VALUES (1,45,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageMickey1942193446.jpeg'),(2,90,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageTobby1942283922.jpeg'),(3,78,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageasds1947172616.jpg'),(4,54,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageTobby1916172794.jpeg'),(5,556,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageMickey1924529035.jpeg'),(6,454,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageScreenshot_2018-03-03-22-15-281927124897.png'),(7,34,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageibi_persona1931382170.png'),(8,37,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImagealevy_avatar_14501332211933035540.jpg'),(9,89,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageuyer-persona-inbound-marketing1928578188.jpg'),(10,45,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageibi_persona1933140308.png'),(11,123,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageIMG-20170820-WA0010[78]1945226683.jpg'),(12,45,'D:Visual Studio 2017 ProjectsTestLoginTestLoginImageIMG-20170820-WA0010[78]1920571686.jpg');
/*!40000 ALTER TABLE `images` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `token_audit`
--

DROP TABLE IF EXISTS `token_audit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `token_audit` (
  `TokenAuditID` int(11) NOT NULL AUTO_INCREMENT,
  `AddedUser` varchar(80) NOT NULL,
  `Category` varchar(50) NOT NULL,
  `AddedDate` text NOT NULL,
  PRIMARY KEY (`TokenAuditID`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_audit`
--

LOCK TABLES `token_audit` WRITE;
/*!40000 ALTER TABLE `token_audit` DISABLE KEYS */;
INSERT INTO `token_audit` VALUES (1,'dddd','fgfgf','2019-06-30 19:34:31'),(2,'Dinuwan','safety','5'),(3,'Dinuwan','safety','5'),(4,'ffg','Sustainability','5'),(5,'ISSCS_V_1._2.Models.UserLogin','Safety','5'),(6,'dinuwan@gmail.com','Sustainability','5'),(7,'ashan@gmail.com','Safety','2019-06-30 22:08:15'),(8,'dinuwan@gmail.com','Sustainability','2019-06-30 23:20:51'),(9,'dinuwan@gmail.com','Sustainability','2019-07-01 00:21:29'),(10,'dinuwan@gmail.com','Sustainability','2019-07-01 00:23:59'),(11,'dinuwan@gmail.com','Safety','2019-07-01 00:32:23'),(12,'ashan@gmail.com','Sustainability','2019-07-01 00:33:45'),(13,'ashan@gmail.com','Sustainability','2019-07-01 02:04:56'),(14,'dinuwan@gmail.com','Sustainability','2019-07-01 11:59:20'),(15,'dinuwan@gmail.com','Sustainability','2019-07-01 13:31:32'),(16,'dinuwan@gmail.com','Safety','2019-07-02 22:29:56'),(17,'dinuwan@gmail.com','Sustainability','2019-07-02 22:33:43'),(18,'damith@gmail.com','hfhfkjdfjkdf','2019-07-05 09:49:27'),(19,'damith@gmail.com','wewe','2019-07-05 09:50:26'),(20,'dinuwan@gmail.com','sd','2019-07-05 09:52:25'),(21,'dinuwan@gmail.com','ddff','2019-07-05 09:53:14'),(22,'damith@gmail.com','fd','2019-07-05 09:55:01'),(23,'damith@gmail.com','Safety','2019-07-05 10:12:13'),(24,'damith@gmail.com','Safety','2019-07-05 10:28:07'),(25,'ashan@gmail.com','Sustainability','2019-07-05 10:36:18'),(26,'dinuwan@gmail.com','Safety','2019-07-06 09:31:38'),(27,'dinuwan@gmail.com','Safety','2019-07-09 15:46:26'),(28,'dinuwan@gmail.com','Sustainability','2019-07-09 18:13:27'),(29,'dinuwan@gmail.com','Safety','2019-07-09 18:39:46'),(30,'dinuwan@gmail.com','Sustainability','2019-07-09 18:45:38'),(31,'dinuwan@gmail.com','Sustainability','2019-07-10 14:04:13'),(32,'damith@gmail.com','Sustainability','2019-07-10 15:17:42'),(33,'damith@gmail.com','Safety','2019-07-10 15:33:17'),(34,'dinuwan@gmail.com','Sustainability','2019-07-14 08:37:38'),(35,'jayan@gmail.com','Sustainability','2019-07-21 21:28:24'),(36,'jayan@gmail.com','Sustainability','2019-07-22 14:21:02'),(37,'jayan@gmail.com','Sustainability','2019-07-22 14:35:46'),(38,'jayan@gmail.com','Safety','2019-07-22 14:37:58'),(39,'dinuwan@gmail.com','Safety','2019-07-22 20:16:16'),(40,'ashan@gmail.com','Sustainability','2019-07-24 23:12:30'),(41,'dinuwan@gmail.com','Sustainability','2019-07-29 16:54:19'),(42,'budhdhika@maholdings.com','Safety','2019-07-30 09:41:04'),(43,'budhdhika@maholdings.com','Sustainability','2019-08-25 09:01:49'),(44,'budhdhika@maholdings.com','Sustainability','2019-08-25 09:02:09');
/*!40000 ALTER TABLE `token_audit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `token_flow`
--

DROP TABLE IF EXISTS `token_flow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `token_flow` (
  `TokenFlowID` int(11) NOT NULL AUTO_INCREMENT,
  `TokenAuditID` int(11) NOT NULL,
  `TokenManagerStatus` varchar(200) NOT NULL,
  `DeptLeaderStatus` varchar(100) NOT NULL,
  `FinalVerification` varchar(45) NOT NULL,
  PRIMARY KEY (`TokenFlowID`),
  UNIQUE KEY `TokenFlowID_UNIQUE` (`TokenFlowID`),
  UNIQUE KEY `TokenAuditID_UNIQUE` (`TokenAuditID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_flow`
--

LOCK TABLES `token_flow` WRITE;
/*!40000 ALTER TABLE `token_flow` DISABLE KEYS */;
INSERT INTO `token_flow` VALUES (2,37,'Production VSM 04','Pending','Pending'),(3,38,'Production VSM 03','Pending','Pending'),(4,39,'Technical','Pending','Pending'),(5,40,'Production Engineering','Pending','Pending'),(6,41,'Factory Engineering','Pending','Pending'),(7,42,'Pending','Pending','Pending'),(8,43,'Quality','Pending','Pending'),(9,44,'Pending','Pending','Pending');
/*!40000 ALTER TABLE `token_flow` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `token_image`
--

DROP TABLE IF EXISTS `token_image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `token_image` (
  `TokenImageID` int(11) NOT NULL AUTO_INCREMENT,
  `TokenID` int(10) unsigned NOT NULL,
  `ImagePath` text NOT NULL,
  PRIMARY KEY (`TokenImageID`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_image`
--

LOCK TABLES `token_image` WRITE;
/*!40000 ALTER TABLE `token_image` DISABLE KEYS */;
INSERT INTO `token_image` VALUES (34,37,'~/Image/18921962-1903825633193612-2862064419361595657-n_orig193546532.jpg'),(35,37,'~/Image/01464375933-petrol-hatti-takibi-ve-kayip-kacak-tespiti193546632.jpg'),(36,38,'~/Image/electric-safety-01193757225.jpeg'),(37,38,'~/Image/17-1024x682193758718.jpg'),(38,39,'~/Image/safety first191615932.jpg'),(39,39,'~/Image/hazard-poison-sign-animated-gif-5191615939.gif'),(40,40,'~/Image/1191228958.jpg'),(41,40,'~/Image/water1191229018.jpg'),(42,41,'~/Image/01464375933-petrol-hatti-takibi-ve-kayip-kacak-tespiti195418780.jpg'),(43,41,'~/Image/electric-safety-01195418818.jpeg'),(44,42,'~/Image/56-apple-512194104045.png'),(45,42,'~/Image/1200px-.NET_Core_Logo.svg194104048.png'),(46,43,'~/Image/water1190146849.jpg'),(47,43,'~/Image/0190146885.jpg'),(48,44,'~/Image/water1190209849.jpg'),(49,44,'~/Image/0190209855.jpg');
/*!40000 ALTER TABLE `token_image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `token_review`
--

DROP TABLE IF EXISTS `token_review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `token_review` (
  `TokenReviewID` int(11) NOT NULL AUTO_INCREMENT,
  `TokenAuditID` int(11) NOT NULL,
  `SpecialActs` text,
  `RepairDepartment` varchar(100) NOT NULL,
  `SentDate` text NOT NULL,
  `SentUser` varchar(45) NOT NULL,
  PRIMARY KEY (`TokenReviewID`),
  UNIQUE KEY `TokenAuditID_UNIQUE` (`TokenAuditID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `token_review`
--

LOCK TABLES `token_review` WRITE;
/*!40000 ALTER TABLE `token_review` DISABLE KEYS */;
INSERT INTO `token_review` VALUES (13,38,'Urgent','Production VSM 03','2019-07-28 11:15:09','dinuwan@gmail.com'),(14,37,'Urgent','Production VSM 04','2019-08-03 16:14:35','dinuwan@gmail.com'),(15,41,'Urgent','Factory Engineering','2019-07-30 10:30:03','jayan@gmail.com'),(16,40,'Urgent','Production Engineering','2019-07-30 09:18:54','dinuwan@gmail.com'),(17,39,'Urgent','Technical','2019-08-01 15:50:51','budhdhika@maholdings.com'),(18,43,'Urgent','Quality','2019-08-25 12:04:39','dinuwan@gmail.com');
/*!40000 ALTER TABLE `token_review` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tokens`
--

DROP TABLE IF EXISTS `tokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tokens` (
  `TokenID` int(11) NOT NULL AUTO_INCREMENT,
  `TokenAuditID` int(10) unsigned NOT NULL,
  `ProblemName` varchar(50) NOT NULL,
  `Location` varchar(80) NOT NULL,
  `AttentionLevel` int(11) NOT NULL,
  `Description` text,
  PRIMARY KEY (`TokenID`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tokens`
--

LOCK TABLES `tokens` WRITE;
/*!40000 ALTER TABLE `tokens` DISABLE KEYS */;
INSERT INTO `tokens` VALUES (30,37,'Liquid Leak','New Office Building',45,'ඉක්මන් පිලිසකර කිරීමක් අවශ්යයි	                '),(31,38,'විදුලි කාන්දුවක්/බිත්ති ඉරි තැලීමක්','නව ගොඩනැගිල්ල',71,'ඉක්මනින් පිලිසකර කරන්න..අවදානම් තත්වයක්\r\n										                '),(32,39,'Water Leak','A Block',42,'Need Quick Action\r\n										                '),(33,40,'Central line leak','Engineering Department Building',73,'Need a quick reparation\r\n										                '),(34,41,'Central Oil leak','New Building',68,'aaasasasas\r\n										                '),(35,42,'Fire','Office Building',100,'Dangerous Up                                                               \r\n										                \r\n										                \r\n										                \r\n										                \r\n										                '),(36,43,'Central Mismatching1','New A3 Building2',52,'ASASAS ASFSBSHFG JSHDJSHDJS SHSIDHSIDHS SLJLPJF JXXBCNXCB IAIADIAS\r\n										                '),(37,44,'Central Mismatching','New A3 Building6',52,'ASASAS ASFSBSHFG JSHDJSHDJS SHSIDHSIDHS SLJLPJF JXXBCNXCB IAIADIAS\r\n										                ');
/*!40000 ALTER TABLE `tokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(45) NOT NULL,
  `UserEmail` varchar(100) NOT NULL,
  `UserMobile` int(11) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `ConfirmPassword` varchar(100) NOT NULL,
  `UserDepartment` varchar(100) NOT NULL,
  `UserType` varchar(40) NOT NULL,
  `UserImage` text,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Dinuwan Kalubowila','dinuwan@gmail.com',712184518,'asdf','asdf','MOS','Token Manager',NULL),(2,'Dinuwan Klaubowila','kalubowila@gmail.com',712184518,'123','123','RM','Employee','~/userimages/android-icon-21190529854.png'),(3,'Hilary Kalubowila','hkkalubowila@live.com',777610400,'123','123','RM','Employee','System.Web.HttpPostedFileWrapper'),(4,'Samitha Perera','kalubowila@live.com',712184518,'123','123','Cutting','Employee','System.Web.HttpPostedFileWrapper'),(5,'Buddhi Kalubowila','buddhi@yahoo.com',712187042,'123','123','Cutting','Department Leader',NULL),(6,'Ashan Perera','ashan@gmail.com',712184518,'123','123','Factory Engineering','Department Leader',NULL),(7,'Damith Perera','damith@gmail.com',712184518,'123','123','Autonomation','Factory Management',NULL),(8,'Jayan Perera','jayan@gmail.com',712184518,'456','456','FG','Token Manager',NULL),(9,'Hiranya Buddhi','budhdhika@maholdings.com',712987042,'123','123','Factory Engineering','Administrator',NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-08-26 21:28:47
