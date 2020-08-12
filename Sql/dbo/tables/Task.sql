CREATE TABLE [dbo].[Task] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Subject] VARCHAR(255) NOT NULL,
    [IsComplete] bit NOT NULL,
    [AssignedToId] UNIQUEIDENTIFIER,
    CONSTRAINT fk_Task_Member_Id
                        FOREIGN KEY ([AssignedToId])
                          REFERENCES [dbo].[Member] ([Id])
)