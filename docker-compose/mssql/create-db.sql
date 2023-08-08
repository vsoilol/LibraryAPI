IF NOT EXISTS (
    SELECT
        *
    FROM
        sys.databases
    WHERE
        name = 'LibraryDatabase'
) BEGIN CREATE DATABASE [LibraryDatabase]
END
GO