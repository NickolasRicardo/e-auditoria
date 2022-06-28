use Locadora;

insert into Cliente(nome, cpf, dataNascimento) values
("Leandro  Nunes","63297827092","1963-02-25"),
("Juan Thomas Baptista","61163358290","1969-05-25"),
("Isabella Laura Dias","13362347258","1975-12-23"),
("Fernanda Raquel Giovanna","32163629732","2000-02-25"),
("Renan Arthur  Vieira","08852425802","1993-05-21")

INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Homem de Ferro', '12', '0');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Homem de Ferro 2', '12', '0');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Homem de Ferro 3', '12', '0');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Homem-Aranha: De volta ao lar', '12', '0');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Homem-Aranha: Longe de casa', '12', '0');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Homem-Aranha: Sem volta para casa', '12', '1');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Minions', '0', '0');
INSERT INTO `locadora`.`filme` (`Titulo`, `ClassificacaoIndicativa`, `Lancamento`) VALUES ('Minions 2', '0', '1');



INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('1', '1', '2021-06-07', '2021-06-10');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('1', '3', '2021-08-01', '2021-06-10');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('1', '5', '2022-06-20', '0001-01-01');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('2', '4', '2022-06-23', '0001-01-01');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('2', '3', '2021-08-01', '0001-01-01');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('3', '2', '2022-06-23', '2022-06-25');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('3', '3', '2021-08-01', '0001-01-01');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('4', '1', '2022-06-25', '0001-01-01');
INSERT INTO `locadora`.`locacao` (`Id_Cliente`, `Id_Filme`, `DataLocacao`, `DataDevolucao`) VALUES ('4', '2', '2022-06-24', '0001-01-01');
