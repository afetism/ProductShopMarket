using MyApp.Data.Models.EntityModel;
using ProductShopMarketAdmin.Command;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProductShopMarketAdmin.ViewModels;

public class ProductVM:BaseViewModel
{
	private bool _isPopupOpen;
	public bool IsPopupOpen
	{
		get { return _isPopupOpen; }
		set
		{
			_isPopupOpen = value;
			OnPropertyChanged(nameof(IsPopupOpen));
		}
	}

	public  RelayCommand OpenPopupCommand { get; set; }
	public  RelayCommand SaveChangesCommand { get; set; }
	public  RelayCommand ClosePopupCommand { get; set; }
	public  RelayCommand UpdateEntityCommand { get; set; }
	public  RelayCommand DeleteEntityCommand { get; set; }
	private ICommand _uploadFileCommand;
	public ICommand UploadFileCommand
	{
		get
		{
			return _uploadFileCommand ??= new RelayCommand(UploadFile);
		}
	}

	private ObservableCollection<Category> categories = [];
	public ObservableCollection<Category> Categories
	{
		get => categories;
		set
		{
			categories=value;
			OnPropertyChanged(nameof(Categories));
		}
	}

	private ObservableCollection<Product> allProducts = [];
	public ObservableCollection<Product> AllProducts
	{
		get => allProducts;
		set
		{
			allProducts=value;
			OnPropertyChanged(nameof(AllProducts));
		}
	}

	private ObservableCollection<Product> filterProducts = [];
	public ObservableCollection<Product> FilterProducts
	{
		get => filterProducts;
		set
		{
			filterProducts=value;
			OnPropertyChanged(nameof(FilterProducts));
		}
	}
	private int quantity;
	[Required(ErrorMessage = "Quantity is Required")]
	
	public int Quantity
	{
		get => quantity;
		set
		{
			quantity=value;
			OnPropertyChanged(nameof(Quantity));
		}
	}
	private decimal price;
	[Required(ErrorMessage = "Price is Required")]
	
	public decimal Price
	{
		get => price;
		set
		{
			price=value;
			OnPropertyChanged(nameof(Price));
		}
	}
	private string name;
	[Required(ErrorMessage = "Name is Required")]
	public string Name
	{
		get => name;
		set
		{
			name=value;
			OnPropertyChanged(nameof(Name));
		}
	}
	private byte[] _photoBytes;
	[Required(ErrorMessage = "Photo is Required")]
	public byte[] PhotoBytes
	{
		get { return _photoBytes; }
		set
		{
			_photoBytes = value;
			OnPropertyChanged(nameof(PhotoBytes));
		}
	}

	private Product productData;
	public Product ProductData
	{
		get => productData;
		set
		{
			productData=value;
			OnPropertyChanged(nameof(ProductData));
		}
	}

	private Category category;
	[Required(ErrorMessage = "Category is Required")]
	public Category Category
	{
		get => category;
		set
		{
			category=value;
			OnPropertyChanged(nameof(Category));
		}
	}



	private PhotoProduct _photo;
	public PhotoProduct Photo
	{
		get => _photo;
		set
		{
			_photo = value;
			OnPropertyChanged(nameof(Photo));
		}
	}
	private string? searchText;
	private int countProduct;

	public string? SearchText
	{
		get => searchText;
	    set
		{
			searchText=value;
			OnPropertyChanged($"{nameof(SearchText)}");
			Load();
		}
	}

	public int CountProduct { 
		get => countProduct;
		set 
		{
			countProduct=value;
			OnPropertyChanged(nameof(CountProduct));
			
		}
    }

	public ProductVM()
    {
		Load();
		OpenPopupCommand = new RelayCommand(OpenPopup);
		SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
		ClosePopupCommand = new RelayCommand(ClosePopup);
		UpdateEntityCommand = new RelayCommand(UpdateEntity);
		DeleteEntityCommand=new RelayCommand(DeleteEntity);
		Photo = new();
		
	}

	
	public void Load()
	{
		if (string.IsNullOrEmpty(SearchText))
		{
			FilterProducts=new ObservableCollection<Product>([.. myAppDbContext.Products]);
			CountProduct= myAppDbContext.Products.Count();
		}
		else
		{
			FilterProducts=new ObservableCollection<Product>(myAppDbContext.Products.ToList().Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
			CountProduct=myAppDbContext.Products.ToList().Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).Count();

		}
		Categories=new ObservableCollection<Category>([.. myAppDbContext.Categories]);

	}
	private void UploadFile(object ibj)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
		};

		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			var fileBytes = File.ReadAllBytes(openFileDialog.FileName);
			if (fileBytes.Length < 2097152)
			{
				Photo.Bytes = fileBytes;
				Photo.Description = Path.GetFileName(openFileDialog.FileName);
				Photo.FileExtension = Path.GetExtension(openFileDialog.FileName);
				Photo.Size = fileBytes.Length;
			}
			else
			{
				System.Windows.MessageBox.Show("The file is too large.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			PhotoBytes = File.ReadAllBytes(openFileDialog.FileName);
		}
	}
	

	private void DeleteEntity(object obj)
	{
		try
		{
			var product = obj as Product;
			if (product is not null)
			{
				var result = System.Windows.MessageBox.Show($"Do you want to delete {product.Name}", "Delete Category", MessageBoxButton.YesNo);
				if (result==MessageBoxResult.Yes)
				{
					myAppDbContext.Products.Remove(product);

					
					var productId = product.Id;
					   
					var photo =myAppDbContext.PhotoProducts.FirstOrDefault(e=>e.ProductId == productId);
					
					myAppDbContext.PhotoProducts.Remove(photo);


					myAppDbContext.SaveChanges();

					Load();

				}
			}
		}
		catch (Exception ex)
		{


			System.Windows.MessageBox.Show("Error:"+ex.Message);
		}
	}

	private void UpdateEntity(object obj)
	{
		var product = obj as Product;
		if (product is not null)
		{
			OpenPopup(product);
		}
	}

	private void ClosePopup(object obj)
	{

		IsPopupOpen = false;
		Name=string.Empty;
		Price=0;
		Quantity=0;
		PhotoBytes=null;
		Photo=null;
		Photo=new();
		Category=null;
	}

	private bool CanSaveChanges(object arg) => Validator.TryValidateObject(this, new ValidationContext(this), null);

	private void SaveChanges(object obj)
	{
		bool isSaved = false;
		try
		{


			var product = obj as Product;
			if (product != null)
			{
				if (product.Id == 0)
				{
					product = new Product() { Name=Name, Quantity=Quantity, Price=Price, CategoryId=Category.Id, Photo=Photo };
					Photo.ProductId = product.Id;
					myAppDbContext.Products.Add(product);
					myAppDbContext.PhotoProducts.Add(Photo);

				}
				else
				{
					product.Name=Name;
					product.Quantity=Quantity;
					product.Price=Price;
					product.CategoryId=product.CategoryId;
					product.Photo=Photo;

					myAppDbContext.Update(product);
					myAppDbContext.PhotoProducts.Update(Photo);

				}
				myAppDbContext.SaveChanges();
				


				isSaved = true;
				Load();


			}

		}
		catch (Exception)
		{


		}
		finally
		{
			if (isSaved)
				ClosePopup(obj);

		}
	}

	private void OpenPopup(object obj)
	{
		try
		{
			Load();
			if (obj is null || obj is not Product objAsProduct)
				ProductData = new();
			else
			{
				ProductData =objAsProduct;
				Name = ProductData.Name;
				Quantity = ProductData.Quantity;
				Price=ProductData.Price;
				Category=ProductData.Category;
				PhotoBytes = ProductData.Photo.Bytes;


			}

			IsPopupOpen = true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}


	}
}
