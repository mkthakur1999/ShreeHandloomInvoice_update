using ShreeHandloomInvoice.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

public class InvoiceModel : BaseViewModel
{
    public InvoiceModel()
    {
        Items = new ObservableCollection<InvoiceItemModel>();
        Items.CollectionChanged += Items_CollectionChanged;
    }

    // ================= ITEMS =================
    public ObservableCollection<InvoiceItemModel> Items { get; }

    private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (InvoiceItemModel item in e.NewItems)
                item.PropertyChanged += Item_PropertyChanged;

        if (e.OldItems != null)
            foreach (InvoiceItemModel item in e.OldItems)
                item.PropertyChanged -= Item_PropertyChanged;

        RecalculateTotals();
    }

    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InvoiceItemModel.Amount) ||
            e.PropertyName == nameof(InvoiceItemModel.Quantity) ||
            e.PropertyName == nameof(InvoiceItemModel.Rate))
        {
            RecalculateTotals();
        }
    }

    // ================= TRANSPORT =================
    private double _transportation;
    public double Transportation
    {
        get => _transportation;
        set
        {
            if (SetProperty(ref _transportation, value))
                RecalculateTotals();
        }
    }

    // ================= TAX % =================
    public double SGSTPercent { get; set; } = 9;
    public double CGSTPercent { get; set; } = 9;
    public double IGSTPercent { get; set; } = 0;

    // ================= CALCULATED TOTALS =================
    public double SubTotal => Items.Sum(i => i.Amount);

    public double SGSTAmount => SubTotal * SGSTPercent / 100;
    public double CGSTAmount => SubTotal * CGSTPercent / 100;
    public double IGSTAmount => SubTotal * IGSTPercent / 100;

    public double GrandTotal =>
        SubTotal + Transportation + SGSTAmount + CGSTAmount + IGSTAmount;

    private void RecalculateTotals()
    {
        OnPropertyChanged(nameof(SubTotal));
        OnPropertyChanged(nameof(SGSTAmount));
        OnPropertyChanged(nameof(CGSTAmount));
        OnPropertyChanged(nameof(IGSTAmount));
        OnPropertyChanged(nameof(GrandTotal));

        TotalAmount = GrandTotal;
    }

    // ================= COMPANY DETAILS =================
    public string ShopName { get; set; } = "SHREE HANDLOOM HOUSE";

    public string ShopAddress { get; set; } =
        "Shop no. 17 E/2, Highway Park CHS. Ltd., Kandivali (E), Mumbai – 400 101";

    public string GSTIN { get; set; } = "27BNHPS9363R1ZR";
    public string PAN { get; set; }
    public string StateName { get; set; } = "Maharashtra";
    public string StateCode { get; set; } = "27";
    public string VatTin { get; set; }
    public string CstTin { get; set; }
    public string UdyogAadhaar { get; set; }
    public string ContactNo { get; set; }
    public string Email { get; set; }

    // ================= BANK DETAILS =================
    public string BankName { get; set; }
    public string BankAccountNo { get; set; }
    public string BankBranch { get; set; }
    public string IFSC { get; set; }

    // ================= INVOICE INFO =================
    private string _invoiceNo = "";
    public string InvoiceNo
    {
        get => _invoiceNo;
        set => SetProperty(ref _invoiceNo, value);
    }

    private DateTime _invoiceDate = DateTime.Now;
    public DateTime InvoiceDate
    {
        get => _invoiceDate;
        set => SetProperty(ref _invoiceDate, value);
    }

    public string OrderNo { get; set; }
    public DateTime? OrderDate { get; set; }
    public string DispatchThrough { get; set; }
    public string VehicleNo { get; set; }
    public string Destination { get; set; }
    public string PoWoNum { get; set; }

    // ================= BUYER / CONSIGNEE =================
    public string BuyerName { get; set; }
    public string BuyerGSTIN { get; set; }
    public string BuyerAddress { get; set; }

    public string ConsigneeName { get; set; }
    public string ConsigneeAddress { get; set; }

    // ================= FINAL =================
    private double _totalAmount;
    public double TotalAmount
    {
        get => _totalAmount;
        set => SetProperty(ref _totalAmount, value);
    }

    private string _amountInWords = "";
    public string AmountInWords
    {
        get => _amountInWords;
        set => SetProperty(ref _amountInWords, value);
    }
}
