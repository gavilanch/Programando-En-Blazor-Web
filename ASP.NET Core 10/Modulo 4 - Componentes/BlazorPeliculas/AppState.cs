using Microsoft.AspNetCore.Components;

namespace BlazorPeliculas;

public class AppState
{
    public string Color { get; set; } = "red";
}

public class AppStateService
{
    public AppState AppState { get; set; } = new();

    public CascadingValueSource<AppState> Source { get; }

    public AppStateService()
    {
        Source = new CascadingValueSource<AppState>(AppState, isFixed: false);
    }

    public Task NotifyChangedAsync() => Source.NotifyChangedAsync();
}