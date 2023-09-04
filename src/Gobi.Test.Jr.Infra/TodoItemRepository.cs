using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace Gobi.Test.Jr.Infra
{
	public class TodoItemRepository : ITodoItemRepository
	{
		public TodoItemRepository()
		{
            CreateDatabase();
			CreateTable();
		}

		private static SQLiteCommand CreateCommand()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");
			var connectionString = $"Data Source={filePath}; Version=3;";
			var connection = new SQLiteConnection(connectionString);

			return new SQLiteCommand(connection);
		}

		private void CreateDatabase()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");

			if (File.Exists(filePath) is false)
			{
				SQLiteConnection.CreateFile(filePath);
			}			
		}

		private void CreateTable()
		{
			var command = CreateCommand();

			command.CommandText = """
                CREATE TABLE IF NOT EXISTS "TodoItem" 
                (
                    "Id" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    "Description" TEXT NOT NULL,
                    "Completed" integer NOT NULL
                );
                """;

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();
		}

	
		public List<TodoItem> GetAll()
		{
			var items = new List<TodoItem>();

            var command = CreateCommand();

            try
            {
                command.Connection.Open();

                command.CommandText = "select Id, Description, Completed from TodoItem";

                System.Data.SQLite.SQLiteDataReader reader = command.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var item = new TodoItem();

                        item.Id = Convert.ToInt32(reader["Id"]);
						item.Description = reader["Description"].ToString();
                        item.Completed = Convert.ToInt32(reader["Completed"]);


                        items.Add(item);

					}
				}

                var itensOrdenados = items.OrderByDescending(item => item.Completed == 1).ToList();

                return itensOrdenados;
            }
            catch
            {
				return null;
            }
            finally { command.Connection.Close(); }
		}

        public bool Add(TodoItem dados)
        {
			bool retorno = false;

            var command = CreateCommand();

            try
			{
                command.Connection.Open();
				command.CommandText = $"insert into TodoItem (Description, Completed) values ('{dados.Description}', '{dados.Completed}')";
                command.ExecuteNonQuery();
				retorno = true;
				return retorno;

            }
            catch
			{
				return false;
			}finally { command.Connection.Close(); }
        }

        public bool Edit(TodoItem dados)
        {
			bool retorno = false;

			var command = CreateCommand();

			try
			{
				command.Connection.Open();
                command.CommandText = $"update TodoItem set Description = '{dados.Description}', Completed = '{dados.Completed}' where Id = {dados.Id}";
                command.ExecuteNonQuery();
                retorno = true;
                return retorno;

            }
            catch (Exception ex) { return false; }
			finally { command.Connection.Close(); }
        }

        public void Delete(int id)
        {
            var command = CreateCommand();

            try
            {
                command.Connection.Open();
                command.CommandText = $"delete from TodoItem where Id = {id}";
                command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally { command.Connection.Close(); }
        }
    }
}
