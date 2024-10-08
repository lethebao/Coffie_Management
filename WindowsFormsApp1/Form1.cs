using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Customer> customers = new List<Customer>();
        private List<Product> products = new List<Product>();
        private List<Order> orders = new List<Order>();
        private List<InventoryItem> inventory = new List<InventoryItem>();

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Coffee Shop Management";
            this.Size = new System.Drawing.Size(800, 600);

            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            TabPage customersTab = new TabPage("Customers");
            TabPage productsTab = new TabPage("Products");
            TabPage ordersTab = new TabPage("Orders");

            tabControl.TabPages.Add(customersTab);
            tabControl.TabPages.Add(productsTab);
            tabControl.TabPages.Add(ordersTab);

            this.Controls.Add(tabControl);

            // Customers Tab
            ListBox customerList = new ListBox();
            customerList.Dock = DockStyle.Left;
            customerList.Width = 200;
            customersTab.Controls.Add(customerList);

            Button addCustomerButton = new Button();
            addCustomerButton.Text = "Add Customer";
            addCustomerButton.Location = new System.Drawing.Point(220, 20);
            addCustomerButton.Click += (sender, e) => AddCustomer(customerList);
            customersTab.Controls.Add(addCustomerButton);

            // Products Tab
            ListBox productList = new ListBox();
            productList.Dock = DockStyle.Left;
            productList.Width = 200;
            productsTab.Controls.Add(productList);

            Button addProductButton = new Button();
            addProductButton.Text = "Add Product";
            addProductButton.Location = new System.Drawing.Point(220, 20);
            addProductButton.Click += (sender, e) => AddProduct(productList);
            productsTab.Controls.Add(addProductButton);

            // Orders Tab
            ListBox orderList = new ListBox();
            orderList.Dock = DockStyle.Left;
            orderList.Width = 200;
            ordersTab.Controls.Add(orderList);

            Button addOrderButton = new Button();
            addOrderButton.Text = "Add Order";
            addOrderButton.Location = new System.Drawing.Point(220, 20);
            addOrderButton.Click += (sender, e) => AddOrder(orderList);
            ordersTab.Controls.Add(addOrderButton);

            TabPage inventoryTab = new TabPage("Inventory");
            tabControl.TabPages.Add(inventoryTab);

            // Inventory Tab
            ListBox inventoryList = new ListBox();
            inventoryList.Dock = DockStyle.Left;
            inventoryList.Width = 300;
            inventoryTab.Controls.Add(inventoryList);

            Button addInventoryButton = new Button();
            addInventoryButton.Text = "Add Inventory Item";
            addInventoryButton.Location = new System.Drawing.Point(320, 20);
            addInventoryButton.Click += (sender, e) => AddInventoryItem(inventoryList);
            inventoryTab.Controls.Add(addInventoryButton);

            Button updateInventoryButton = new Button();
            updateInventoryButton.Text = "Update Inventory";
            updateInventoryButton.Location = new System.Drawing.Point(320, 50);
            updateInventoryButton.Click += (sender, e) => UpdateInventoryItem(inventoryList);
            inventoryTab.Controls.Add(updateInventoryButton);
        }


        private void AddInventoryItem(ListBox inventoryList)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter item name:", "Add Inventory Item");
            if (!string.IsNullOrEmpty(name))
            {
                string quantityStr = Microsoft.VisualBasic.Interaction.InputBox("Enter quantity:", "Add Inventory Item");
                if (int.TryParse(quantityStr, out int quantity))
                {
                    InventoryItem item = new InventoryItem { Name = name, Quantity = quantity };
                    inventory.Add(item);
                    RefreshInventoryList(inventoryList);
                }
            }
        }

        private void UpdateInventoryItem(ListBox inventoryList)
        {
            if (inventoryList.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }

            InventoryItem selectedItem = inventory[inventoryList.SelectedIndex];
            string quantityStr = Microsoft.VisualBasic.Interaction.InputBox($"Enter new quantity for {selectedItem.Name}:", "Update Inventory Item", selectedItem.Quantity.ToString());

            if (int.TryParse(quantityStr, out int newQuantity))
            {
                selectedItem.Quantity = newQuantity;
                RefreshInventoryList(inventoryList);
            }
        }

        private void RefreshInventoryList(ListBox inventoryList)
        {
            inventoryList.Items.Clear();
            foreach (var item in inventory)
            {
                inventoryList.Items.Add($"{item.Name} - Quantity: {item.Quantity}");
            }
        }

        private void AddCustomer(ListBox customerList)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter customer name:", "Add Customer");
            if (!string.IsNullOrEmpty(name))
            {
                Customer customer = new Customer { Name = name };
                customers.Add(customer);
                customerList.Items.Add(customer.Name);
            }
        }

        private void AddProduct(ListBox productList)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Enter product name:", "Add Product");
            if (!string.IsNullOrEmpty(name))
            {
                string priceStr = Microsoft.VisualBasic.Interaction.InputBox("Enter product price:", "Add Product");
                if (decimal.TryParse(priceStr, out decimal price))
                {
                    Product product = new Product { Name = name, Price = price };
                    products.Add(product);
                    productList.Items.Add($"{product.Name} - ${product.Price}");
                }
            }
        }

        private void AddOrder(ListBox orderList)
        {
            if (customers.Count == 0 || products.Count == 0)
            {
                MessageBox.Show("Please add customers and products first.");
                return;
            }

            Customer customer = customers[new Random().Next(customers.Count)];
            Product product = products[new Random().Next(products.Count)];
            Order order = new Order { Customer = customer, Product = product, Date = DateTime.Now };
            orders.Add(order);
            orderList.Items.Add($"{order.Date.ToShortDateString()} - {customer.Name} - {product.Name}");
        }
    }

    public class Customer
    {
        public string Name { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public DateTime Date { get; set; }
    }

    public class InventoryItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}