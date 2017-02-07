namespace SuperShoes.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SuperShoes.Models.SuperShoesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SuperShoes.Models.SuperShoesContext context)
        {
            context.Stores.AddOrUpdate(x => x.id,
                new Store() { id = 1, name = "Store 1", address = "Address 1" },
                new Store() { id = 2, name = "Store 2", address = "Address 2" },
                new Store() { id = 3, name = "Store 3", address = "Address 3" }
            );

            context.Articles.AddOrUpdate(x => x.id,
                new Article()
                {
                    id = 1,
                    name = "Article 1",
                    description = "Description 1",
                    StoreId = 1,
                    price = 10.10M,
                    total_in_shelf = 10,
                    total_in_vault = 10
                },
                new Article()
                {
                    id = 2,
                    name = "Article 2",
                    description = "Description 2",
                    StoreId = 2,
                    price = 20.20M,
                    total_in_shelf = 20,
                    total_in_vault = 20
                },
                new Article()
                {
                    id = 3,
                    name = "Article 3",
                    description = "Description 3",
                    StoreId = 3,
                    price = 30.30M,
                    total_in_shelf = 30,
                    total_in_vault = 30
                },
                new Article()
                {
                    id = 4,
                    name = "Article 4",
                    description = "Description 4",
                    StoreId = 1,
                    price = 40.40M,
                    total_in_shelf = 40,
                    total_in_vault = 40
                }
            );
        }
    }
}
