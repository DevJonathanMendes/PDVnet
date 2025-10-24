# PDVnet - Gestão de Produtos (WPF)

## Pré-requisitos

- Windows 8.1 ou superior (ou equivalente que suporte WPF).  
- Visual Studio 2019/2022 com carga de trabalho **Desenvolvimento para Desktop com .NET**. 
- **SQL Server 2017 com LocalDB** (instalado automaticamente com o Visual Studio).  

> LocalDB é usado para armazenar os dados; o banco é criado automaticamente ao executar o projeto.

---

## Rodando o projeto

1. Clone o repositório:

```bash
git clone https://github.com/DevJonathanMendes/PDVnet.git
```

(Ou na interface do seu Visual Studio)

No Visual Studio, basta iniciar o projeto.

## Script SQL (Criação, caso queira no SSMS)

```SQL
CREATE DATABASE PDVnetDB;
GO

USE PDVnetDB;
GO

CREATE TABLE Produto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255),
    Preco DECIMAL(10,2) NOT NULL,
    Quantidade INT NOT NULL,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);
```
