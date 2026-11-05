using ContosoPizza.Models;

namespace ContosoPizza.Services;

public static class PizzaService
{
    private static readonly List<Pizza> Pizzas =
    [
        new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
        new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true },
        new Pizza { Id = 3, Name = "Pepperoni", IsGlutenFree = false }
    ];

    private static int _nextId = 4;

    public static List<Pizza> GetAll() => Pizzas;

    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(pizza => pizza.Id == id);

    public static void Add(Pizza pizza)
    {
        pizza.Id = _nextId++;
        Pizzas.Add(pizza);
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is not null)
        {
            Pizzas.Remove(pizza);
        }
    }

    public static void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(existingPizza => existingPizza.Id == pizza.Id);
        if (index >= 0)
        {
            Pizzas[index] = pizza;
        }
    }
}
