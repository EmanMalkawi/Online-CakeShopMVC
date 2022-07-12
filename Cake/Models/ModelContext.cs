using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cake.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Cakeshop> Cakeshops { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Visa> Visas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=train_user121;PASSWORD=Eman@2021;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TRAIN_USER121")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutusid)
                    .HasName("SYS_C00129350");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Aboutusid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTUSID");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("FACEBOOK");

                entity.Property(e => e.Instagram)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("INSTAGRAM");

                entity.Property(e => e.Location)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Mobilenumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MOBILENUMBER");

                entity.Property(e => e.Shopid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SHOPID");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("TWITTER");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Aboutus)
                    .HasForeignKey(d => d.Shopid)
                    .HasConstraintName("ABOUTUSID_FK");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("BILL");

                entity.Property(e => e.Billid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BILLID");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDERDATE");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Productname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Custid)
                    .HasConstraintName("BILLCUSTID_FK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("BILLORDERID_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("BILLPRODUCTID_FK");
            });

            modelBuilder.Entity<Cakeshop>(entity =>
            {
                entity.ToTable("CAKESHOP");

                entity.Property(e => e.Cakeshopid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CAKESHOPID");

                entity.Property(e => e.Cakeshopname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAKESHOPNAME");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("CART");

                entity.Property(e => e.Cartid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CARTID");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Item)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ITEM");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.Custid)
                    .HasConstraintName("CARTCUSTID_FK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("CARTORDERID_FK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.Contactid)
                    .HasName("SYS_C00129353");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Contactid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTACTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Message)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Shopid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SHOPID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Contactus)
                    .HasForeignKey(d => d.Shopid)
                    .HasConstraintName("CONTACTUSID_FK");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Custid)
                    .HasName("SYS_C00129360");

                entity.ToTable("CUSTOMER");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Custemail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSTEMAIL");

                entity.Property(e => e.Custfname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSTFNAME");

                entity.Property(e => e.Custlname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSTLNAME");

                entity.Property(e => e.Custroleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTROLEID");

                entity.HasOne(d => d.Custrole)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Custroleid)
                    .HasConstraintName("CUSTROLEID_FK");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("SYS_C00129357");

                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMPID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Positionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POSITIONID");

                entity.Property(e => e.Positionname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("POSITIONNAME");

                entity.Property(e => e.Salary)
                    .HasPrecision(6)
                    .HasColumnName("SALARY");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Positionid)
                    .HasConstraintName("EMPPOSITIONID_FK");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("FEEDBACK");

                entity.Property(e => e.Feedbackid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FEEDBACKID");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Text)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Custid)
                    .HasConstraintName("FEEDBACKCUSTID_FK");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Aboutustext)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("ABOUTUSTEXT");

                entity.Property(e => e.Backgroundtext)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("BACKGROUNDTEXT");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Shopid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SHOPID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.Shopid)
                    .HasConstraintName("HOMESHOPID_FK");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.Property(e => e.Loginid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGINID");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EMPID");

                entity.Property(e => e.Loginroleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LOGINROLEID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Custid)
                    .HasConstraintName("LOGINCUSTID_FK");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Empid)
                    .HasConstraintName("LOGINEMPID_FK");

                entity.HasOne(d => d.Loginrole)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.Loginroleid)
                    .HasConstraintName("LOGINROLEID_FK");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Oederdate)
                    .HasColumnType("DATE")
                    .HasColumnName("OEDERDATE");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Productname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Custid)
                    .HasConstraintName("ORDERCUSTID_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("ORDERPRODUCTID_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productcategid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTCATEGID");

                entity.Property(e => e.Productdescription)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTDESCRIPTION");

                entity.Property(e => e.Productname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.HasOne(d => d.Productcateg)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Productcategid)
                    .HasConstraintName("PRODUCTCARTEGID_FK");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("REPORT");

                entity.Property(e => e.Reportid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REPORTID");

                entity.Property(e => e.Enddate)
                    .HasColumnType("DATE")
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Productname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Startdate)
                    .HasColumnType("DATE")
                    .HasColumnName("STARTDATE");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("REPORTORDERID_FK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("REPORTPRODUCTID_FK");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Visa>(entity =>
            {
                entity.ToTable("VISA");

                entity.Property(e => e.Visaid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("VISAID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Custid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CUSTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Visas)
                    .HasForeignKey(d => d.Custid)
                    .HasConstraintName("VISACUSTID_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
