using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1AlsWPF
{
    public partial class MainWindow : Window
    {
        Order _order;
        Ticket _ticketAB;
        Ticket _ticketBC;
        Ticket _ticketABC;
        public MainWindow()
        {
            InitializeComponent();
            InitializeEvents();

            _order = new Order();
            _ticketAB = new Ticket("Singel ticket AB", 2.90M);
            _ticketBC = new Ticket("Single ticket BC", 3.30M);
            _ticketABC = new Ticket("Single ticket ABC", 3.60M);
        }

        // Click-events
        private void InitializeEvents()
        {
            AddTicketAB.Click += AddTicket_Click;
            RemTicketAB.Click += RemTicket_Click;
            AddTicketBC.Click += AddTicket_Click;
            RemTicketBC.Click += RemTicket_Click;
            AddTicketABC.Click += AddTicket_Click;
            RemTicketABC.Click += RemTicket_Click;

            Money01.Click += AddMoney_Click;
            Money02.Click += AddMoney_Click;
            Money05.Click += AddMoney_Click;
            Money1.Click += AddMoney_Click;
            Money2.Click += AddMoney_Click;
            Money5.Click += AddMoney_Click;
            Money10.Click += AddMoney_Click;
            Money20.Click += AddMoney_Click;

            Cancel.Click += Cancel_Click;
            Submit.Click += Submit_Click;
            Again.Click += Again_Click;
        }
        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            addTicket(button.Name.ToString());
        }
        private void RemTicket_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            removeTicket(button.Name.ToString());
        }
        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            hideAlert();
            Button button = sender as Button;

            switch (button.Name.ToString())
            {
                case "Money01":
                    _order.totalBalance += 0.10M;
                    updateBalance();
                    break;
                case "Money02":
                    _order.totalBalance += 0.20M;
                    updateBalance();
                    break;
                case "Money05":
                    _order.totalBalance += 0.50M;
                    updateBalance();
                    break;
                case "Money1":
                    _order.totalBalance += 1;
                    updateBalance();
                    break;
                case "Money2":
                    _order.totalBalance += 2;
                    updateBalance();
                    break;
                case "Money5":
                    _order.totalBalance += 5;
                    updateBalance();
                    break;
                case "Money10":
                    _order.totalBalance += 10;
                    updateBalance();
                    break;
                case "Money20":
                    _order.totalBalance += 20;
                    updateBalance();
                    break;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            cancelOperation();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_order.canBuy())
            {
                buyTickets();
            }
            else
            {
                showAlert("not enough money", true);
            }
        }
        private void Again_Click(object sender, RoutedEventArgs e)
        {
            cancelOperation();
            hideAlert();
            hideShowEverything(false);
            Again.Visibility = Visibility.Hidden;
        }
        

        public void buyTickets()
        {
            decimal diff = _order.totalDiff;
            showAlert($"Your change: {diff.ToString()} €", false);
        }
        public void cancelOperation()
        {
            _order.totalBalance = 0;
            _order.totalPrice = 0;
            updateBalance();
            updateTotal();

            _ticketAB.quantity = 0;
            QuantityTicketAB.Content = "0";

            _ticketBC.quantity = 0;
            QuantityTicketBC.Content = "0";

            _ticketABC.quantity = 0;
            QuantityTicketABC.Content = "0";
        }


        public void addTicket(string ticketName)
        {
            switch(ticketName)
            {
                case "AddTicketAB":
                    _ticketAB.quantity++;
                    QuantityTicketAB.Content = _ticketAB.quantity.ToString();
                    _order.totalPrice += _ticketAB.price;
                    updateTotal();
                    break;
                case "AddTicketBC":
                    _ticketBC.quantity++;
                    QuantityTicketBC.Content = _ticketBC.quantity.ToString();
                    _order.totalPrice += _ticketBC.price;
                    updateTotal();
                    break;
                case "AddTicketABC":
                    _ticketABC.quantity++;
                    QuantityTicketABC.Content = _ticketABC.quantity.ToString();
                    _order.totalPrice += _ticketABC.price;
                    updateTotal();
                    break;
            }
        }
        public void removeTicket(string ticketName)
        {
            switch (ticketName)
            {
                case "RemTicketAB":
                    if (_ticketAB.quantity > 0)
                    {
                        _ticketAB.quantity--;
                        QuantityTicketAB.Content = _ticketAB.quantity.ToString();
                        _order.totalPrice -= _ticketAB.price;
                        updateTotal();
                    }
                    break;
                case "RemTicketBC":
                    if (_ticketBC.quantity > 0)
                    {
                        _ticketBC.quantity--;
                        QuantityTicketBC.Content = _ticketBC.quantity.ToString();
                        _order.totalPrice -= _ticketBC.price;
                        updateTotal();
                    }
                    break;
                case "RemTicketABC":
                    if (_ticketABC.quantity > 0)
                    {
                        _ticketABC.quantity--;
                        QuantityTicketABC.Content = _ticketABC.quantity.ToString();
                        _order.totalPrice -= _ticketABC.price;
                        updateTotal();
                    }
                    break;
            }
        }
        
        // hide-show window elements
        private void hideShowEverything(bool hide)
        {
            Visibility vis;
            if (hide)
            {
                vis = Visibility.Hidden;
            } else
            {
                vis = Visibility.Visible;
            }

            Button[] buttons = { AddTicketAB, RemTicketAB, AddTicketBC, RemTicketBC, AddTicketABC, RemTicketABC, Money01, Money02, Money05, Money1, Money2, Money5, Money10, Money20, Cancel, Submit };
            foreach(Button button in buttons)
            {
                button.Visibility = vis;
            }

            Label[] labels = { PriceAB, PriceBC, PriceABC, Money, TicketAB, TicketBC, TicketABC, QuantityTicketAB, QuantityTicketBC, QuantityTicketABC, TotalTotal, TotalPrice, BalanceBalance, TotalBalance };
            foreach (Label label in labels)
            {
                label.Visibility = vis;
            }
        }
        public void showAlert(string text, bool error)
        {
            LabelFehler.Content = text;
            if (error)
            {
                LabelFehler.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFF0000"));
            }
            else {
                LabelFehler.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#22BD2F00"));
                hideShowEverything(true);
                Again.Visibility = Visibility.Visible;
            }
            LabelFehler.Visibility = Visibility.Visible;
        }
        public void hideAlert()
        {
            LabelFehler.Visibility = Visibility.Hidden;
        }
        
        // update window element values
        private void updateTotal()
        {
            TotalPrice.Content = $"{_order.totalPrice.ToString()} €";
        }
        private void updateBalance()
        {
            TotalBalance.Content = $"{_order.totalBalance.ToString()} €";
        }
        
        // classes
        public class Ticket
        {
            public string name { get; }
            public decimal price { get; }
            public int quantity { get; set; }
            public Ticket(string name, decimal price)
            {
                this.name = name;
                this.price = price;
            }
        }
        public class Order
        {
            public decimal totalPrice { get; set; } = 0.00M;
            public decimal totalBalance { get; set; } = 0.00M;
            public decimal totalDiff { get { return totalBalance - totalPrice; } }
            public bool canBuy()
            {
                if (totalDiff < 0)
                {
                    return false;
                } 
                else
                {
                    return true;
                }
            }

        }

    }
}
