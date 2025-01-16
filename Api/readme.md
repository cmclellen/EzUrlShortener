# URL Shortnener

1. Ensure to run the following SQL to allow the container app access to Azure SQL:
   ```sql
   CREATE USER [ca-vnsxt6qwqbeks-dev] FROM EXTERNAL PROVIDER;
   ALTER ROLE db_datareader ADD MEMBER [ca-vnsxt6qwqbeks-dev];
   ALTER ROLE db_datawriter ADD MEMBER [ca-vnsxt6qwqbeks-dev];
   ALTER ROLE db_ddladmin ADD MEMBER [ca-vnsxt6qwqbeks-dev];
   ```
1. Access swagger using the URL `https://ca-vnsxt6qwqbeks-dev.wonderfulsmoke-5e15e657.australiaeast.azurecontainerapps.io/index.html`
