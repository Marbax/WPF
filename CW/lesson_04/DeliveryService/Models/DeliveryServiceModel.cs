namespace DeliveryService.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DeliveryServiceModel : DbContext
    {
        // Your context has been configured to use a 'DeliveryServiceModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DeliveryService.Models.DeliveryServiceModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DeliveryServiceModel' 
        // connection string in the application configuration file.
        public DeliveryServiceModel()
            : base("name=DeliveryServiceModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}