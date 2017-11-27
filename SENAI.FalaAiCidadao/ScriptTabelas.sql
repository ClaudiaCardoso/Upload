CREATE TABLE [dbo].[Admins] (
    [AdminId] [uniqueidentifier] NOT NULL,
    [Email] [varchar](50) NOT NULL,
    [Senha] [varchar](50) NOT NULL,
    CONSTRAINT [PK_dbo.Admins] PRIMARY KEY ([AdminId])
)
CREATE TABLE [dbo].[Comentarios] (
    [ComentarioId] [uniqueidentifier] NOT NULL,
    [TextoPost] [varchar](1000) NOT NULL,
    [Ativo] [bit] NOT NULL,
    [Excluido] [bit] NOT NULL,
    [PostagemId] [uniqueidentifier] NOT NULL,
    [EleitorId] [uniqueidentifier] NOT NULL,
    [PoliticoId] [uniqueidentifier] NOT NULL,
    [Eleitor_UsuarioId] [uniqueidentifier],
    CONSTRAINT [PK_dbo.Comentarios] PRIMARY KEY ([ComentarioId])
)
CREATE TABLE [dbo].[Eleitores] (
    [UsuarioId] [uniqueidentifier] NOT NULL,
    [Nome] [varchar](50) NOT NULL,
    [Sobrenome] [varchar](50) NOT NULL,
    [CPF] [varchar](11) NOT NULL,
    [DataCadastro] [datetime] NOT NULL,
    [DataNascimento] [datetime] NOT NULL,
    [Email] [varchar](20) NOT NULL,
    [Senha] [varchar](50) NOT NULL,
    CONSTRAINT [PK_dbo.Eleitores] PRIMARY KEY ([UsuarioId])
)
CREATE TABLE [dbo].[Enderecos] (
    [EnderecoId] [uniqueidentifier] NOT NULL,
    [Estado] [varchar](50) NOT NULL,
    [Cidade] [varchar](50) NOT NULL,
    [Bairro] [varchar](50) NOT NULL,
    [Logradouro] [varchar](50) NOT NULL,
    [Numero] [varchar](5) NOT NULL,
    [Cep] [varchar](8) NOT NULL,
    [Complemento] [varchar](50) NOT NULL,
    [EleitorId] [uniqueidentifier] NOT NULL,
    [Eleitor_UsuarioId] [uniqueidentifier],
    CONSTRAINT [PK_dbo.Enderecos] PRIMARY KEY ([EnderecoId])
)
CREATE TABLE [dbo].[Politicos] (
    [PoliticoId] [uniqueidentifier] NOT NULL,
    [Nome] [varchar](50) NOT NULL,
    [CPF] [varchar](11) NOT NULL,
    [Partido] [varchar](50) NOT NULL,
    [DataCadastro] [datetime] NOT NULL,
    [DataNascimento] [datetime] NOT NULL,
    [Email] [varchar](50) NOT NULL,
    [Senha] [varchar](50) NOT NULL,
    CONSTRAINT [PK_dbo.Politicos] PRIMARY KEY ([PoliticoId])
)
CREATE TABLE [dbo].[Postagens] (
    [PostagemId] [uniqueidentifier] NOT NULL,
    [TituloPost] [varchar](50) NOT NULL,
    [TextoPost] [varchar](1000) NOT NULL,
    [EleitorId] [uniqueidentifier] NOT NULL,
    [RegiaoId] [uniqueidentifier] NOT NULL,
    [TipoId] [uniqueidentifier] NOT NULL,
    [Ativo] [bit] NOT NULL,
    [Excluido] [bit] NOT NULL,
    [Inativo] [bit] NOT NULL,
    [Eleitor_UsuarioId] [uniqueidentifier],
    CONSTRAINT [PK_dbo.Postagens] PRIMARY KEY ([PostagemId])
)
CREATE TABLE [dbo].[ImagemPost] (
    [ImagemPostId] [uniqueidentifier] NOT NULL,
    [PostagemId] [uniqueidentifier] NOT NULL,
    [CaminhoImagem] [varchar](100) NOT NULL,
    CONSTRAINT [PK_dbo.ImagemPost] PRIMARY KEY ([ImagemPostId])
)
CREATE TABLE [dbo].[Regioes] (
    [RegiaoId] [uniqueidentifier] NOT NULL,
    [Nome] [varchar](20) NOT NULL,
    CONSTRAINT [PK_dbo.Regioes] PRIMARY KEY ([RegiaoId])
)
CREATE TABLE [dbo].[Tipoes] (
    [TipoId] [uniqueidentifier] NOT NULL,
    [Descicao] [varchar](100),
    CONSTRAINT [PK_dbo.Tipoes] PRIMARY KEY ([TipoId])
)

ALTER TABLE [dbo].[Comentarios] ADD CONSTRAINT [FK_dbo.Comentarios_dbo.Eleitores_Eleitor_UsuarioId] FOREIGN KEY ([Eleitor_UsuarioId]) REFERENCES [dbo].[Eleitores] ([UsuarioId])
ALTER TABLE [dbo].[Comentarios] ADD CONSTRAINT [FK_dbo.Comentarios_dbo.Politicos_PoliticoId] FOREIGN KEY ([PoliticoId]) REFERENCES [dbo].[Politicos] ([PoliticoId]) ON DELETE CASCADE
ALTER TABLE [dbo].[Comentarios] ADD CONSTRAINT [FK_dbo.Comentarios_dbo.Postagens_PostagemId] FOREIGN KEY ([PostagemId]) REFERENCES [dbo].[Postagens] ([PostagemId]) ON DELETE CASCADE
ALTER TABLE [dbo].[Enderecos] ADD CONSTRAINT [FK_dbo.Enderecos_dbo.Eleitores_Eleitor_UsuarioId] FOREIGN KEY ([Eleitor_UsuarioId]) REFERENCES [dbo].[Eleitores] ([UsuarioId])
ALTER TABLE [dbo].[Postagens] ADD CONSTRAINT [FK_dbo.Postagens_dbo.Eleitores_Eleitor_UsuarioId] FOREIGN KEY ([Eleitor_UsuarioId]) REFERENCES [dbo].[Eleitores] ([UsuarioId])
ALTER TABLE [dbo].[Postagens] ADD CONSTRAINT [FK_dbo.Postagens_dbo.Regioes_RegiaoId] FOREIGN KEY ([RegiaoId]) REFERENCES [dbo].[Regioes] ([RegiaoId]) ON DELETE CASCADE
ALTER TABLE [dbo].[Postagens] ADD CONSTRAINT [FK_dbo.Postagens_dbo.Tipoes_TipoId] FOREIGN KEY ([TipoId]) REFERENCES [dbo].[Tipoes] ([TipoId]) ON DELETE CASCADE
ALTER TABLE [dbo].[ImagemPost] ADD CONSTRAINT [FK_dbo.ImagemPost_dbo.Postagens_PostagemId] FOREIGN KEY ([PostagemId]) REFERENCES [dbo].[Postagens] ([PostagemId]) ON DELETE CASCADE

