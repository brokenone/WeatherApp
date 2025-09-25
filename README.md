# 🌤 WeatherApp

Учебное консольное приложение на **.NET**, которое получает данные о погоде из разных API и демонстрирует:
- работу с **Dependency Injection (DI)**,
- модульную архитектуру (провайдеры для разных сервисов),
- конфигурацию через **appsettings.json**,
- подключение **HttpClient** и расширяемость,
- возможность кэширования и подключения базы данных.

---

## 🚀 Возможности
- Получение текущей погоды по городу из нескольких API (OpenWeather, WeatherAPI, Tomorrow.io).  
- Определение минимальной и максимальной температуры по списку городов.  
- Гибкая настройка ключей и URL в `appsettings.json`.  
- Поддержка Dependency Injection для удобной замены и расширения сервисов.  
- (В будущем) Кэширование, база данных для хранения истории и прогнозов.

---

## 🛠 Технологии
- [.NET 9](https://dotnet.microsoft.com/)  
- `Microsoft.Extensions.DependencyInjection`  
- `Microsoft.Extensions.Configuration`  
- `HttpClient`  
- (планируется) SQLite / EF Core  
- (планируется) MemoryCache

---

## 🚀 Запуск проекта

Клонируйте репозиторий и запустите проект:

```bash
git clone https://github.com/username/WeatherApp.git
cd WeatherApp
dotnet restore
dotnet run
```
---

## 📂 Структура проекта
```plaintext
WeatherApp/
│── Program.cs              // точка входа
│── appsettings.json        // конфигурация API ключей и URL
│
├── Services/
│   ├── WeatherAggregator.cs  // агрегатор провайдеров
│   ├── IWeatherProvider.cs
│   ├── IWeatherService.cs
│   ├── OpenWeatherProvider.cs
│   ├── WeatherApiProvider.cs
│   ├── TomorrowApiProvider.cs
│   ├── ICityService.cs
│   └── CityService.cs
│
├── Models/
│   ├── CitiesResponse.cs
│   ├── OpenWeatherResponse.cs
│   ├── TomorrowApiResponse.cs
│   ├── WeatherApiResponse.cs
│   └── WeatherResult.cs
│
└── Config/
    └── ApiSettings.cs
```
---

## 🔑 Настройка API ключей
- В файле appsettings.json укажи свои ключи:
```
"ApiSettings": {
  "OpenWeatherApiKey": "YOUR_KEY",
  "WeatherApiKey": "YOUR_KEY",
  "TomorrowApiKey": "YOUR_KEY",
  "OpenWeatherApiUrl": "https://api.openweathermap.org/data/2.5/weather",
  "WeatherApiUrl": "http://api.weatherapi.com/v1/current.json",
  "TomorrowApiUrl": "https://api.tomorrow.io/v4/weather/realtime",
  "CitiesApiUrl": "https://countriesnow.space/api/v0.1/countries/cities"
}
