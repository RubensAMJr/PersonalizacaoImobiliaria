## Sistema de Personalização de Unidades Imobiliárias - API RESTful

Projeto API RESTful desenvolvido em ASP.NET CORE 9 com clean architecture e CQS(Command–query separation) para o gerenciamento de personalizações de unidades imobiliarias.

A arquitetura foi escolhida por familiaridade com o desenvolviemto com a mesma em outros projetos.

## Como Rodar o Projeto Localmente

### 1- Subir o Banco de Dados PostgreSQL com Docker

```bash
docker-compose up
```

### 2 - Atualizar o banco de dados com as migrations

```powershell
Update-Database
```

### 3 - Ao rodar a aplicação realizar o Login como Administrador ou Usuário (O sistema não possui cadastro de usuário)

![image](https://github.com/user-attachments/assets/cc106c8f-bf28-4994-b68f-0941d0ede76d)

![image](https://github.com/user-attachments/assets/dca37221-5823-48e4-9253-0254f1b29bb9)

## Lista de Endpoints

| Rota | Descrição | Perfil |
| --- | --- |  --- |
| 🟩 `/auth/login`  | Rota de autenticação JWT | `Sistema` |
| 🟩 `/solicitacao` | Rota para criar uma solicitação de Personalização de uma Unidade Imobiliaria | `Cliente` |
| 🟦 `/solicitacao/listar` | Rota para listar solicitações criadas por Unidade Imobiliaria | `Administrador` |
| 🟩 `/unidade`  | Rota para cadastrar uma Unidade Imobiliaria | `Administrador` |
| 🟩 `/unidade/personalizacao` | Rota para cadastrar uma opção de personalização imobiliaria | `Administrador` |
| 🟦 `/unidade/personalizacao/listar` | Rota para listar Personalizações disponíveis no sistema | `Cliente` |
