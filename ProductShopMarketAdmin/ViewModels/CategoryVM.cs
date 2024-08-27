using MyApp.Data.Data;
using MyApp.Data.Models.EntityModel;
using ProductShopMarketAdmin.Command;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ProductShopMarketAdmin.ViewModels;

public class CategoryVM:BaseViewModel
{
	private ObservableCollection<Category> allcategories;
	public ObservableCollection<Category> AllCategories
	{
		get => allcategories;
		set
		{
			allcategories=value;
			OnPropertyChanged(nameof(AllCategories));
		}

	}
	private ObservableCollection<Category> filterCategory;
	public ObservableCollection<Category> FilterCategory
	{
		get => filterCategory;
		set
		{
			filterCategory=value;
			OnPropertyChanged(nameof(FilterCategory));
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

	private Category categoryData;
	public Category CategoryData
	{
		get => categoryData;
		set
		{
			categoryData=value;
			OnPropertyChanged(nameof(CategoryData));
		}

	}

	public string SearchText
	{
		get => searchText;
		set
		{
			searchText=value;
			OnPropertyChanged(nameof(SearchText));
			Load();
		}
    }

	private bool _isPopupOpen;
	private int countCategories;
	private string searchText;

	public bool IsPopupOpen
	{
		get { return _isPopupOpen; }
		set
		{
			_isPopupOpen = value;
			OnPropertyChanged(nameof(IsPopupOpen));
		}
	}

	public int CountCategories { 
		get => countCategories;
		set {
			countCategories=value;
			OnPropertyChanged(nameof(CountCategories));
		}

	}


	public RelayCommand OpenPopupCommand { get; set; }
	public RelayCommand SaveChangesCommand { get; set; }
	public RelayCommand ClosePopupCommand { get; set; }
	public RelayCommand UpdateEntityCommand { get; set; }
	public RelayCommand DeleteEntityCommand { get; set; }

	public CategoryVM()
    {
		Load();
		OpenPopupCommand=new(executeopenPopup);
		SaveChangesCommand=new(executesaveChanges, CanSaveChanges);
		ClosePopupCommand=new(executeclosePopup);
		UpdateEntityCommand=new(executeupdateEntity);
		DeleteEntityCommand=new(executedeleteEntity);
		
	}

	private bool CanSaveChanges(object arg) => Validator.TryValidateObject(this, new ValidationContext(this), null);

	private void executedeleteEntity(object obj)
	{
		try
		{
			var category = obj as Category;
			if (category is not null)
			{
				var result = MessageBox.Show($"Do you want to delete {category.Name}", "Delete Category", MessageBoxButton.YesNo);
				if (result==MessageBoxResult.Yes)
				{
					myAppDbContext.Categories.Remove(category);
					myAppDbContext.SaveChanges();
					Load();

				}
			}
		}
		catch (Exception ex)
		{


			MessageBox.Show("Error:"+ex.Message);
		}
	}

	private void executeupdateEntity(object obj)
	{
		var category = obj as Category;
		if (category is not  null)
		{
			executeopenPopup(category);
		}
	}

	private void executeclosePopup(object obj)
	{
		Name=string.Empty;
		IsPopupOpen=false;
		
	}

	private void executesaveChanges(object obj)
	{
		bool isSaved = false;
		try
		{


			var category = obj as Category;
			if (category != null)
			{
				if (category.Id == 0)
				{
					category = new Category() { Name = Name };
					myAppDbContext.Categories.Add(category);

				}
				else
				{
					category.Name = Name;
					myAppDbContext.Categories.Update(category);
				}

				myAppDbContext.SaveChanges();
				isSaved = true;
				Load();
			}
			else
			{

			}

		}
		catch (Exception)
		{


		}
		finally
		{
			if (isSaved)
				executeclosePopup(obj);

		}
	}

	private void executeopenPopup(object obj)
	{
		try
		{
			var category = obj as Category;
			if (category is null)
			{
				CategoryData=new Category();
				
			}
			else
			{
				CategoryData = category;
				Name = CategoryData.Name;


			}
			IsPopupOpen=true;
		}
		catch
		{
			MessageBox.Show("Hass error");
		}
	}

	public void Load()
	{
		if(string.IsNullOrEmpty(SearchText))
		{
			FilterCategory=new ObservableCollection<Category>([.. myAppDbContext.Categories]);
			CountCategories= myAppDbContext.Categories.Count();
		}
		else
		{
			FilterCategory=new ObservableCollection<Category>(myAppDbContext.Categories.ToList().Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
			CountCategories=myAppDbContext.Categories.ToList().Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).Count();

		}
		
	}

















}
