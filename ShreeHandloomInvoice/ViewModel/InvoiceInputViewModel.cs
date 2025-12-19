using ShreeHandloomInvoice.Command;
using ShreeHandloomInvoice.Converter;
using ShreeHandloomInvoice.Data;
using ShreeHandloomInvoice.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShreeHandloomInvoice.ViewModel
{
    public class InvoiceInputViewModel : BaseViewModel
    {
        //public InvoiceInputViewModel()
        //{
        //    Invoice = new InvoiceModel();

        //    NewItem = new InvoiceItemModel();

        //    AddItemCommand = new RelayCommand(AddItem);
        //    GenerateInvoiceCommand = new RelayCommand(GenerateInvoice);
        //}
        public InvoiceInputViewModel()
        {
            //Invoice = new InvoiceModel
            //{
            //    Items = new ObservableCollection<InvoiceItemModel>()
            //};
            Invoice = new InvoiceModel();

            NewItem = new InvoiceItemModel();

            AddItemCommand = new RelayCommand(AddItem);
            GenerateInvoiceCommand = new RelayCommand(GenerateInvoice);
        }


        // ---------------- INVOICE MAIN MODEL ----------------
        private InvoiceModel _invoice;
        public InvoiceModel Invoice
        {
            get => _invoice;
            set => SetProperty(ref _invoice, value);
        }

        // ---------------- NEW ITEM INPUT MODEL ----------------
        private InvoiceItemModel _newItem;
        public InvoiceItemModel NewItem
        {
            get => _newItem;
            set => SetProperty(ref _newItem, value);
        }

        // Commands
        public ICommand AddItemCommand { get; }
        public ICommand GenerateInvoiceCommand { get; }

        // ---------------- ADD ITEM ----------------
        private void AddItem()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(NewItem.Description)) return;
            if (NewItem.Quantity <= 0) return;

            var item = new InvoiceItemModel
            {
                SrNo = Invoice.Items.Count + 1,
                Description = NewItem.Description,
                HSN = NewItem.HSN,
                Quantity = NewItem.Quantity,
                Rate = NewItem.Rate,
                Unit = NewItem.Unit,
                Amount = NewItem.Quantity * NewItem.Rate,
                
                
            };
            //var itm = new InvoiceModel
            //{
            //    ConsigneeName = Invoice.ConsigneeName
            //};
            Invoice.Items.Add(item);

            // Clear input fields
            NewItem = new InvoiceItemModel();
            OnPropertyChanged(nameof(NewItem));
        }

        // ---------------- GENERATE INVOICE ----------------
        //private void GenerateInvoice()
        //{
        //    // Auto calculate amount for each item
        //    foreach (var item in Invoice.Items)
        //    {
        //        item.Amount = item.Quantity * item.Rate;
        //    }

        //    // Total sum
        //    Invoice.TotalAmount = 0;
        //    foreach (var item in Invoice.Items)
        //        Invoice.TotalAmount += item.Amount;

        //    // Convert total to words
        //    Invoice.AmountInWords = AmountToWords.Convert(Invoice.TotalAmount);


        //    // Navigate to Preview Page
        //    NavigationService.Navigate(new InvoicePreviewViewModel(Invoice));
        //}
        private void GenerateInvoice()
        {
            if (Invoice.Items.Count == 0) return;

            foreach (var item in Invoice.Items)
                item.Amount = item.Quantity * item.Rate;

            Invoice.TotalAmount = Invoice.Items.Sum(x => x.Amount);

            Invoice.AmountInWords =
                AmountToWords.Convert(Invoice.TotalAmount);

            // ✅ SAVE TO DB HERE
            SaveInvoice();

            // ✅ Navigate
            NavigationService.Navigate(
                new InvoicePreviewViewModel(Invoice)
            );
        }


        private void SaveInvoice()
        {
            InvoiceRepository repo = new InvoiceRepository();

            // 1️⃣ Save invoice data
            Invoice.InvoiceId = repo.InsertInvoice(Invoice);

            // 2️⃣ Build PDF path
            Invoice.PdfPath =
                InvoicePdfPathService.BuildFullPath(Invoice);

            // ❌ PDF abhi generate mat karo
            // Preview / Print pe generate hoga

            // 3️⃣ Save path
            repo.UpdatePdfPath(Invoice.InvoiceId, Invoice.PdfPath);
        }


    }
}
