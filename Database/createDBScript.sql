-- MySQL Workbench Forward Engineering
SET
  @OLD_UNIQUE_CHECKS = @ @UNIQUE_CHECKS,
  UNIQUE_CHECKS = 0;

SET
  @OLD_FOREIGN_KEY_CHECKS = @ @FOREIGN_KEY_CHECKS,
  FOREIGN_KEY_CHECKS = 0;

SET
  @OLD_SQL_MODE = @ @SQL_MODE,
  SQL_MODE = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema Locadora
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema Locadora
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Locadora` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;

USE `Locadora`;

-- -----------------------------------------------------
-- Table `Locadora`.`cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Locadora`.`cliente` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(200) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `CPF` VARCHAR(11) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `DataNascimento` DATETIME(6) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB AUTO_INCREMENT = 0 DEFAULT CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `Locadora`.`filme`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Locadora`.`filme` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Titulo` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL,
  `ClassificacaoIndicativa` INT NOT NULL,
  `Lancamento` TINYINT(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE = InnoDB AUTO_INCREMENT = 0 DEFAULT CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `Locadora`.`locacao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Locadora`.`locacao` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Id_Cliente` INT NOT NULL,
  `Id_Filme` INT NOT NULL,
  `DataLocacao` DATETIME(6) NOT NULL,
  `DataDevolucao` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_locacao_Id_Cliente` (`Id_Cliente` ASC) VISIBLE,
  INDEX `IX_locacao_Id_Filme` (`Id_Filme` ASC) VISIBLE,
  CONSTRAINT `FK_locacao_cliente_Id_Cliente` FOREIGN KEY (`Id_Cliente`) REFERENCES `Locadora`.`cliente` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_locacao_filme_Id_Filme` FOREIGN KEY (`Id_Filme`) REFERENCES `Locadora`.`filme` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 0 DEFAULT CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

SET
  SQL_MODE = @OLD_SQL_MODE;

SET
  FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS;

SET
  UNIQUE_CHECKS = @OLD_UNIQUE_CHECKS;