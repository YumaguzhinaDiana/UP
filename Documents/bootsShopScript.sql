CREATE DATABASE  IF NOT EXISTS `db_boots_shop` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_boots_shop`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: db_boots_shop
-- ------------------------------------------------------
-- Server version	8.0.40

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
-- Table structure for table `manufacturs`
--

DROP TABLE IF EXISTS `manufacturs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `manufacturs` (
  `manufactur_id` int NOT NULL AUTO_INCREMENT,
  `manufactur_name` varchar(80) DEFAULT NULL,
  PRIMARY KEY (`manufactur_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manufacturs`
--

LOCK TABLES `manufacturs` WRITE;
/*!40000 ALTER TABLE `manufacturs` DISABLE KEYS */;
INSERT INTO `manufacturs` VALUES (1,'Alessio Nesca'),(2,'CROSBY'),(3,'Kari'),(4,'Marco Tozzi'),(5,'Rieker'),(6,'Рос');
/*!40000 ALTER TABLE `manufacturs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_composition`
--

DROP TABLE IF EXISTS `order_composition`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order_composition` (
  `order_composition_id` int NOT NULL AUTO_INCREMENT,
  `oc_order_id` int DEFAULT NULL,
  `oc_tovar_id` varchar(7) DEFAULT NULL,
  `oc_tovar_amount` int DEFAULT NULL,
  PRIMARY KEY (`order_composition_id`),
  KEY `fk_oc_order_id_idx` (`oc_order_id`),
  KEY `fk_oc_tovar_id_idx` (`oc_tovar_id`),
  CONSTRAINT `fk_oc_order_id` FOREIGN KEY (`oc_order_id`) REFERENCES `orders` (`order_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_oc_tovar_id` FOREIGN KEY (`oc_tovar_id`) REFERENCES `tovars` (`tovar_id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_composition`
--

LOCK TABLES `order_composition` WRITE;
/*!40000 ALTER TABLE `order_composition` DISABLE KEYS */;
INSERT INTO `order_composition` VALUES (1,1,'А112Т4',2),(2,2,'H782T5',1),(3,3,'J384T6',10),(4,4,'F572H7',5),(5,5,'А112Т4',2),(6,6,'H782T5',1),(7,7,'J384T6',10),(8,8,'F572H7',5),(9,9,'B320R5',5),(10,10,'S213E3',5),(11,1,'F635R4',2),(12,2,'G783F5',1),(13,3,'D572U8',10),(14,4,'D329H3',4),(15,5,'F635R4',2),(16,6,'G783F5',1),(17,7,'D572U8',10),(18,8,'D329H3',4),(19,9,'G432E4',1),(20,10,'E482R4',5),(23,13,'K358H6',3),(24,13,'А112Т4',7),(25,14,'А112Т4',7),(26,14,'T324F5',6),(27,15,'А112Т4',1),(28,15,'S213E3',1);
/*!40000 ALTER TABLE `order_composition` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `order_id` int NOT NULL,
  `order_date` date DEFAULT NULL,
  `order_delivery_date` date DEFAULT NULL,
  `order_pickup_point` int DEFAULT NULL,
  `order_client_id` int DEFAULT NULL,
  `order_code` varchar(6) DEFAULT NULL,
  `order_status` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`order_id`),
  KEY `fk_order_pickuppoint_idx` (`order_pickup_point`),
  KEY `fk_order_client_id_idx` (`order_client_id`),
  CONSTRAINT `fk_order_client_id` FOREIGN KEY (`order_client_id`) REFERENCES `users` (`user_id`),
  CONSTRAINT `fk_order_pickuppoint` FOREIGN KEY (`order_pickup_point`) REFERENCES `pick_up_points` (`pick_up_point_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,'2025-02-27','2025-04-20',1,8,'901','Завершен'),(2,'2022-09-28','2025-04-21',11,3,'902','Завершен'),(3,'2025-03-21','2025-04-22',2,5,'903','Завершен'),(4,'2025-02-20','2025-04-23',11,4,'904','Завершен'),(5,'2025-03-17','2025-04-24',2,8,'905','Завершен'),(6,'2025-03-01','2025-04-25',15,3,'906','Завершен'),(7,'2025-02-03','2025-04-26',3,5,'907','Завершен'),(8,'2025-03-31','2025-04-27',19,4,'908','Новый'),(9,'2025-04-02','2025-04-28',5,8,'909','Новый'),(10,'2025-04-03','2025-04-29',19,8,'910','Новый'),(13,'2026-02-19','2019-01-31',1,7,'8121','Новый'),(14,'2026-02-19','2026-02-19',1,7,'1536','Новый'),(15,'2026-03-01','2026-03-04',3,9,'2415','Новый');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pick_up_points`
--

DROP TABLE IF EXISTS `pick_up_points`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pick_up_points` (
  `pick_up_point_id` int NOT NULL,
  `pick_up_point_index` varchar(7) DEFAULT NULL,
  `pick_up_point_city` varchar(35) DEFAULT NULL,
  `pick_up_point_street` varchar(30) DEFAULT NULL,
  `pick_up_point_house` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`pick_up_point_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pick_up_points`
--

LOCK TABLES `pick_up_points` WRITE;
/*!40000 ALTER TABLE `pick_up_points` DISABLE KEYS */;
INSERT INTO `pick_up_points` VALUES (1,'420151','г. Лесной','ул. Вишневая','32'),(2,'125061','г. Лесной','ул. Подгорная','8'),(3,'630370','г. Лесной','ул. Шоссейная','24'),(4,'400562','г. Лесной','ул. Зеленая','32'),(5,'614510','г. Лесной','ул. Маяковского','47'),(6,'410542','г. Лесной','ул. Светлая','46'),(7,'620839','г. Лесной','ул. Цветочная','8'),(8,'443890','г. Лесной','ул. Коммунистическая','1'),(9,'603379','г. Лесной','ул. Спортивная','46'),(10,'603721','г. Лесной','ул. Гоголя','41'),(11,'410172','г. Лесной','ул. Северная','13'),(12,'614611','г. Лесной','ул. Молодежная','50'),(13,'454311','г.Лесной','ул. Новая','19'),(14,'660007','г.Лесной','ул. Октябрьская','19'),(15,'603036','г. Лесной','ул. Садовая','4'),(16,'394060','г.Лесной','ул. Фрунзе','43'),(17,'410661','г. Лесной','ул. Школьная','50'),(18,'625590','г. Лесной','ул. Коммунистическая','20'),(19,'625683','г. Лесной','ул. 8 Марта',''),(20,'450983','г.Лесной','ул. Комсомольская','26'),(21,'394782','г. Лесной','ул. Чехова','3'),(22,'603002','г. Лесной','ул. Дзержинского','28'),(23,'450558','г. Лесной','ул. Набережная','30'),(24,'344288','г. Лесной','ул. Чехова','1'),(25,'614164','г.Лесной','ул. Степная','30'),(26,'394242','г. Лесной','ул. Коммунистическая','43'),(27,'660540','г. Лесной','ул. Солнечная','25'),(28,'125837','г. Лесной','ул. Шоссейная','40'),(29,'125703','г. Лесной','ул. Партизанская','49'),(30,'625283','г. Лесной','ул. Победы','46'),(31,'614753','г. Лесной','ул. Полевая','35'),(32,'426030','г. Лесной','ул. Маяковского','44'),(33,'450375','г. Лесной','ул. Клубная','44'),(34,'625560','г. Лесной','ул. Некрасова','12'),(35,'630201','г. Лесной','ул. Комсомольская','17'),(36,'190949','г. Лесной','ул. Мичурина','26');
/*!40000 ALTER TABLE `pick_up_points` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplyers`
--

DROP TABLE IF EXISTS `supplyers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplyers` (
  `supplyer_id` int NOT NULL AUTO_INCREMENT,
  `supplyer_name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`supplyer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplyers`
--

LOCK TABLES `supplyers` WRITE;
/*!40000 ALTER TABLE `supplyers` DISABLE KEYS */;
INSERT INTO `supplyers` VALUES (1,'Kari'),(2,'Обувь для вас');
/*!40000 ALTER TABLE `supplyers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tovar_categories`
--

DROP TABLE IF EXISTS `tovar_categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tovar_categories` (
  `tovar_category_id` int NOT NULL,
  `tovar_category_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`tovar_category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tovar_categories`
--

LOCK TABLES `tovar_categories` WRITE;
/*!40000 ALTER TABLE `tovar_categories` DISABLE KEYS */;
INSERT INTO `tovar_categories` VALUES (1,'Женская обувь'),(2,'Мужская обувь');
/*!40000 ALTER TABLE `tovar_categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tovar_types`
--

DROP TABLE IF EXISTS `tovar_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tovar_types` (
  `tovar_type_id` int NOT NULL AUTO_INCREMENT,
  `tovar_type_name` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`tovar_type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tovar_types`
--

LOCK TABLES `tovar_types` WRITE;
/*!40000 ALTER TABLE `tovar_types` DISABLE KEYS */;
INSERT INTO `tovar_types` VALUES (1,'Ботинки'),(2,'Кеды'),(3,'Кроссовки'),(4,'Полуботинки'),(5,'Сапоги'),(6,'Тапочки'),(7,'Туфли');
/*!40000 ALTER TABLE `tovar_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tovars`
--

DROP TABLE IF EXISTS `tovars`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tovars` (
  `tovar_id` varchar(7) NOT NULL,
  `tovar_type` int DEFAULT NULL,
  `tovar_unit` varchar(6) DEFAULT NULL,
  `tovar_price` decimal(7,2) DEFAULT NULL,
  `tovar_supplyer` int DEFAULT NULL,
  `tovar_manufactur` int DEFAULT NULL,
  `tovar_category` int DEFAULT NULL,
  `tovar_current_discount` float DEFAULT NULL,
  `tovar_storage_amount` int DEFAULT NULL,
  `tovar_description` text,
  `tovar_image` varchar(80) DEFAULT NULL,
  `tovar_status` enum('active','deleted') DEFAULT 'active',
  PRIMARY KEY (`tovar_id`),
  KEY `fk_tovar_type_id_idx` (`tovar_type`),
  KEY `fk_tovar_suplyer_id_idx` (`tovar_supplyer`),
  KEY `fk_tovar_manufactur_id_idx` (`tovar_manufactur`),
  KEY `fk_tovar_category_id_idx` (`tovar_category`),
  CONSTRAINT `fk_tovar_category_id` FOREIGN KEY (`tovar_category`) REFERENCES `tovar_categories` (`tovar_category_id`),
  CONSTRAINT `fk_tovar_manufactur_id` FOREIGN KEY (`tovar_manufactur`) REFERENCES `manufacturs` (`manufactur_id`),
  CONSTRAINT `fk_tovar_suplyer_id` FOREIGN KEY (`tovar_supplyer`) REFERENCES `supplyers` (`supplyer_id`),
  CONSTRAINT `fk_tovar_type_id` FOREIGN KEY (`tovar_type`) REFERENCES `tovar_types` (`tovar_type_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tovars`
--

LOCK TABLES `tovars` WRITE;
/*!40000 ALTER TABLE `tovars` DISABLE KEYS */;
INSERT INTO `tovars` VALUES ('A527W',1,'шт.',1900.00,1,1,1,15,20,'Удобная женская обувь 39 размера черного цвета','Снимок экрана 2026-03-01 232311.png','deleted'),('B320R5',1,'шт.',4300.00,1,1,1,2,6,'Туфли Rieker женские демисезонные, размер 41, цвет коричневый','1.jpg','active'),('B431R5',1,'шт.',2700.00,2,5,2,2,5,'Мужские кожаные ботинки/мужские ботинки','2.jpg','active'),('C436G5',1,'шт.',10200.00,1,1,1,15,9,'Ботинки женские, ARGO, размер 40','3.jpg','active'),('D268G5',7,'шт.',4399.00,2,5,1,3,12,'Туфли Rieker женские демисезонные, размер 36, цвет коричневый','4.jpg','active'),('D329H3',4,'шт.',1890.00,2,1,1,4,4,'Полуботинки Alessio Nesca женские 3-30797-47, размер 37, цвет: бордовый','5.jpg','active'),('D364R4',7,'шт.',12400.00,1,3,1,16,5,'Туфли Luiza Belly женские Kate-lazo черные из натуральной замши','6.jpg','active'),('D572U8',3,'шт.',4100.00,2,6,2,3,6,'129615-4 Кроссовки мужские','7.jpg','active'),('E482R4',4,'шт.',1800.00,1,3,1,2,14,'Полуботинки kari женские MYZ20S-149, размер 41, цвет: черный','8.jpg','active'),('F022M',1,'шт.',87.00,1,1,1,0,55,'','Снимок экрана 2025-03-02 213436_1.png','active'),('F427R5',1,'шт.',11800.00,2,5,1,15,11,'Ботинки на молнии с декоративной пряжкой FRAU','9.jpg','active'),('F572H7',7,'шт.',2700.00,1,4,1,2,14,'Туфли Marco Tozzi женские летние, размер 39, цвет черный','10.jpg','active'),('F635R4',1,'шт.',3244.00,2,4,1,2,13,'Ботинки Marco Tozzi женские демисезонные, размер 39, цвет бежевый',NULL,'active'),('G432E4',7,'шт.',2800.00,1,3,1,3,15,'Туфли kari женские TR-YR-413017, размер 37, цвет: черный',NULL,'active'),('G531F4',1,'шт.',6600.00,1,3,1,12,9,'Ботинки женские зимние ROMER арт. 893167-01 Черный',NULL,'active'),('G783F5',1,'шт.',5900.00,1,6,2,2,8,'Мужские ботинки Рос-Обувь кожаные с натуральным мехом',NULL,'active'),('H535R5',1,'шт.',2300.00,2,5,1,2,7,'Женские Ботинки демисезонные',NULL,'active'),('H782T5',7,'шт.',4499.00,1,3,2,4,5,'Туфли kari мужские классика MYZ21AW-450A, размер 43, цвет: черный',NULL,'active'),('J384T6',1,'шт.',3800.00,2,5,2,2,16,'B3430/14 Полуботинки мужские Rieker',NULL,'active'),('J542F5',6,'шт.',500.00,1,3,2,13,0,'Тапочки мужские Арт.70701-55-67син р.41',NULL,'active'),('J818G',1,'poi',100.00,1,1,1,7,23,'System.Windows.Documents.FlowDocument','fon1.jpg','active'),('K345R4',4,'шт.',2100.00,2,2,2,2,3,'407700/01-02 Полуботинки мужские CROSBY',NULL,'active'),('K358H6',6,'шт.',599.00,1,5,2,20,2,'Тапочки мужские син р.41','close.png','active'),('K546Q',1,'1',1.00,1,1,1,1,1,'rfrfrfrfrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr','камень.png','active'),('L754R4',4,'шт.',1700.00,1,3,1,2,7,'Полуботинки kari женские WB2020SS-26, размер 38, цвет: черный',NULL,'active'),('M018S',1,'шт.',1234.00,1,1,1,45,6,'укукукукуккукукукукукук','Снимок экрана 2025-12-22 222653.png','active'),('M542T5',3,'шт.',2800.00,2,5,2,18,3,'Кроссовки мужские TOFA',NULL,'active'),('N457T5',4,'шт.',4600.00,1,2,1,3,13,'Полуботинки Ботинки черные зимние, мех',NULL,'active'),('O754F4',7,'шт.',5400.00,2,5,1,4,18,'Туфли женские демисезонные Rieker артикул 55073-68/37',NULL,'active'),('P764G4',7,'шт.',6800.00,1,2,1,15,15,'Туфли женские, ARGO, размер 38',NULL,'active'),('R242Q',1,'2',2.00,1,1,1,0,2,'System.Windows.Documents.FlowDocument','fon.jpg','active'),('S213E3',4,'шт.',2156.00,2,2,2,3,6,'407700/01-01 Полуботинки мужские CROSBY',NULL,'active'),('S326R5',6,'шт.',9900.00,2,2,2,17,15,'Мужские кожаные тапочки \"Профиль С.Дали\" ',NULL,'deleted'),('S634B5',2,'шт.',5500.00,2,2,2,3,0,'Кеды Caprice мужские демисезонные, размер 42, цвет черный',NULL,'active'),('T324F5',5,'шт.',4699.00,1,2,1,2,5,'Сапоги замша Цвет: синий',NULL,'active'),('X378X',1,'шт.',800.99,1,1,1,1,9,'лошгрпгнпнгпнгпнггн','Снимок экрана 2025-03-02 213436_2.png','deleted'),('А112Т4',1,'шт.',4990.00,1,3,1,3,6,'Женские Ботинки демисезонные kari',NULL,'active');
/*!40000 ALTER TABLE `tovars` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_roles`
--

DROP TABLE IF EXISTS `user_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user_roles` (
  `user_role_id` int NOT NULL,
  `user_role_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`user_role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_roles`
--

LOCK TABLES `user_roles` WRITE;
/*!40000 ALTER TABLE `user_roles` DISABLE KEYS */;
INSERT INTO `user_roles` VALUES (1,'Авторизированный клиент'),(2,'Администратор'),(3,'Менеджер');
/*!40000 ALTER TABLE `user_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL,
  `user_role` int DEFAULT NULL,
  `user_surname` varchar(30) DEFAULT NULL,
  `user_name` varchar(30) DEFAULT NULL,
  `user_patronymic` varchar(30) DEFAULT NULL,
  `user_login` varchar(60) DEFAULT NULL,
  `user_password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  KEY `fk_user_role_id_idx` (`user_role`),
  CONSTRAINT `fk_user_role_id` FOREIGN KEY (`user_role`) REFERENCES `user_roles` (`user_role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,2,'Никифорова','Весения','Николаевна','94d5ous@gmail.com','uzWC67'),(2,2,'Сазонов','Руслан','Германович','uth4iz@mail.com','2L6KZG'),(3,2,'Одинцов','Серафим','Артёмович','yzls62@outlook.com','JlFRCZ'),(4,3,'Степанов','Михаил','Артёмович','1diph5e@tutanota.com','8ntwUp'),(5,3,'Ворсин','Петр','Евгеньевич','tjde7c@yahoo.com','YOyhfR'),(6,3,'Старикова','Елена','Павловна','wpmrc3do@tutanota.com','RSbvHv'),(7,1,'Михайлюк','Анна','Вячеславовна','5d4zbu@tutanota.com','rwVDh9'),(8,1,'Ситдикова','Елена','Анатольевна','ptec8ym@yahoo.com','LdNyos'),(9,1,'Ворсин','Петр','Евгеньевич','1qz4kw@mail.com','gynQMT'),(10,1,'Старикова','Елена','Павловна','4np6se@mail.com','AtnDjr');
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

-- Dump completed on 2026-03-05 22:55:53
