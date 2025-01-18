
---

# 📚 .NET Framework API with Unity DI and AutoMapper

This project is a **.NET Framework API** application that integrates with **SQL Server**. It uses **Unity** for dependency injection and **AutoMapper** for efficient object mapping.

---

## 🌟 Features

- **Dependency Injection**: Managed by Unity for clean and modular architecture.
- **Database Integration**: SQL Server as the database solution.
- **DTO Implementation**: Simplifies data exchange between layers.
- **AutoMapper Integration**: Reduces boilerplate mapping code.
- **RESTful Endpoints**: Fully functional API endpoints for CRUD operations.

---

## 📋 Prerequisites

1. **Visual Studio 2022**
2. **SQL Server**
3. **NuGet Packages**:
   - Unity
   - AutoMapper
   - AutoMapper.Extensions.Microsoft.DependencyInjection

---

## 🚀 Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/locblognewbiedev/todo-list-dotnet
   cd todo-list-dotnet
   ```

2. Open the project in **Visual Studio 2022**.

3. Configure the **SQL Server** connection string in `Web.config`:
   ```xml
   <connectionStrings>
       <add name="DefaultConnection" 
            connectionString="Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;" 
            providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

4. Install required NuGet packages:
   ```bash
   Install-Package Unity
   Install-Package AutoMapper
   Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
   ```

5. Run the application.

---

## 🛠️ Using DTO and AutoMapper

### 📖 What is a DTO?

A **Data Transfer Object (DTO)** is a simple object used to encapsulate data and send it between processes. Using DTOs helps:
- Prevent exposing your domain models to clients.
- Reduce data transfer size by sending only necessary fields.
- Simplify the structure of your API responses.

---

### 🔧 Setting Up AutoMapper

1. **Create a Mapping Profile**:
   Add a new class `MappingProfile.cs` in the `Mapping` folder:
   ```csharp
   using AutoMapper;
   using MyApiProject.Models;
   using MyApiProject.DTOs;

   public class MappingProfile : Profile
   {
       public MappingProfile()
       {
           // Map Domain to DTO
           CreateMap<Item, ItemDto>();

           // Map DTO to Domain
           CreateMap<ItemDto, Item>();
       }
   }
   ```

2. **Configure AutoMapper in Unity**:
   Update `UnityConfig.cs`:
   ```csharp
   using Unity;
   using AutoMapper;

   public static class UnityConfig
   {
       public static void RegisterComponents()
       {
           var container = new UnityContainer();

           // Register AutoMapper
           var mapperConfig = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<MappingProfile>();
           });

           var mapper = mapperConfig.CreateMapper();
           container.RegisterInstance(mapper);

           // Register other dependencies
           container.RegisterType<IItemService, ItemService>();
           container.RegisterType<IItemRepository, ItemRepository>();

           System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
       }
   }
   ```

---

### 🌟 Example: Using DTO in Controllers

#### Controller Example
```csharp
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using MyApiProject.DTOs;
using MyApiProject.Models;
using MyApiProject.Services;

public class ItemsController : ApiController
{
    private readonly IItemService _itemService;
    private readonly IMapper _mapper;

    public ItemsController(IItemService itemService, IMapper mapper)
    {
        _itemService = itemService;
        _mapper = mapper;
    }

    // GET /api/items
    [HttpGet]
    public IHttpActionResult GetItems()
    {
        var items = _itemService.GetAllItems();
        var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);

        return Ok(itemsDto);
    }

    // POST /api/items
    [HttpPost]
    public IHttpActionResult CreateItem(ItemDto itemDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var item = _mapper.Map<Item>(itemDto);
        _itemService.AddItem(item);

        return Created(new Uri(Request.RequestUri + "/" + item.Id), itemDto);
    }
}
```

---

### 📝 Example DTO Class

Create a simple DTO class that represents the data structure sent to the client:

```csharp
namespace MyApiProject.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
```

---

## 🏗 Project Structure

```
/MyApiProject
├── Controllers       // API controllers
├── DTOs              // Data Transfer Objects
├── Models            // Domain models
├── Services          // Business logic services
├── Repositories      // Data access logic
├── Mapping           // AutoMapper configuration
├── Dependency        // Unity DI configuration
├── Web.config        // Application settings
└── README.md         // Project documentation
```

---

## 🔍 Benefits of Using DTO and AutoMapper

- **Separation of Concerns**: Keeps domain models independent from API responses.
- **Simplicity**: Reduces code duplication by automating mappings.
- **Maintainability**: Easy to adapt to future changes in data structure.

---

This README is now complete and explains **DTOs**, **AutoMapper**, and their integration in a structured, clean format! 😊
