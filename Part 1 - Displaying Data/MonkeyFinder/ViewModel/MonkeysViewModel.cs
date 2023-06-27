namespace MonkeyFinder.ViewModel;
using MonkeyFinder.Services;

public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;    
    public ObservableCollection<Monkey> Monkeys { get; }= new ObservableCollection<Monkey>();   

    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
       
    }
    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if(IsBusy) return;
        try
        {
            IsBusy= true;
           
            var monkeys = await monkeyService.MonkeysAsync();

            if(Monkeys.Count!=0)
            {
                Monkeys.Clear();
            }

            foreach(var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }

        }catch(Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", $"Unable to get Monkeys:{ex.Message}", "OK");

        }
        finally { IsBusy= false; }
    }
}
