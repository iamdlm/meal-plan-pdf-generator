using MealPlanPdfGenerator.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MealPlanPdfGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        private FormEntry CreateFormEntry()
        {
            return new FormEntry()
            {
                Gender = "male",
                Diet = 1,
                MealPlan = new MealPlan()
                {
                    Days = new List<Day>()
                    {
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Spinach",
                                                Quantity = 0.5,
                                                Unit = "cup"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 200,
                                                Unit = "grams"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.OrangeSlices,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 10.5,
                                                Unit = "cups"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                        {
                                            Number = 2,
                                            Title = "Basmati rice with chicken breast",
                                        Image = Images.OrangeSlices,
                                        PrepTime = 2,
                                            CookTime = 12,
                                            DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                            Ingredients = new List<MealIngredient>()
                                            {
                                                new MealIngredient()
                                                {
                                                    Name = "Chicken Breast",
                                                    Quantity = 200,
                                                    Unit = "grams"
                                                },
                                                new MealIngredient()
                                                {
                                                    Name = "Rice",
                                                    Quantity = 100,
                                                    Unit = "grams"
                                                }
                                            },
                                            Preparation = new List<Instruction>()
                                            {
                                                new Instruction()
                                                {
                                                    Index = 1,
                                                    Description = "Cook the chicken"
                                                },
                                                new Instruction()
                                                {
                                                    Index = 2,
                                                    Description = "Cook the rice"
                                                }
                                            }
                                        },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Wrap,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 200,
                                                Unit = "grams"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.OrangeSlices,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 200,
                                                Unit = "grams"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Wrap,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.OrangeSlices,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 200,
                                                Unit = "grams"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Wrap,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.OrangeSlices,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 200,
                                                Unit = "grams"
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        {
                            new Day()
                            {
                                DayNumber = 1,
                                Calories = 2000,
                                Protein = 150,
                                Carbs = 240,
                                Fat = 45,
                                Meals = new List<Meal>()
                                {
                                    new Meal()
                                    {
                                        Number = 1,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Easy,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Wrap,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.OrangeSlices,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.LentilSoup,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Wrap,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    },
                                    new Meal()
                                    {
                                        Number = 2,
                                        Title = "Basmati rice with chicken breast",
                                        Image = Images.Oatmeal,
                                        PrepTime = 2,
                                        CookTime = 12,
                                        DifficultyLevel = Enums.DifficultyLevel.Moderate,
                                        Ingredients = new List<MealIngredient>()
                                        {
                                            new MealIngredient()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 200,
                                                Unit = "grams"
                                            },
                                            new MealIngredient()
                                            {
                                                Name = "Rice",
                                                Quantity = 100,
                                                Unit = "grams"
                                            }
                                        },
                                        Preparation = new List<Instruction>()
                                        {
                                            new Instruction()
                                            {
                                                Index = 1,
                                                Description = "Cook the chicken"
                                            },
                                            new Instruction()
                                            {
                                                Index = 2,
                                                Description = "Cook the rice"
                                            }
                                        }
                                    }
                                },
                                ShoppingList = new List<ShoppingList>()
                                {
                                    new ShoppingList()
                                    {
                                        Category = "Proteins",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Chicken Breast",
                                                Quantity = 400,
                                                Unit = "grams"
                                            }
                                        }
                                    },
                                    new ShoppingList()
                                    {
                                        Category = "Carbs",
                                        Items = new List<ShoppingListItem>()
                                        {
                                            new ShoppingListItem()
                                            {
                                                Name = "Rice",
                                                Quantity = 200,
                                                Unit = "grams"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    ShoppingList = new List<ShoppingList>()
                    {
                        new ShoppingList()
                        {
                            Category = "Proteins",
                            Items = new List<ShoppingListItem>()
                            {
                                new ShoppingListItem()
                                {
                                    Name = "Chicken Breast",
                                    Quantity = 800,
                                    Unit = "grams"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Ground beef",
                                    Quantity = 1000,
                                    Unit = "grams"
                                }
                            }
                        },
                        new ShoppingList()
                        {
                            Category = "Carbs",
                            Items = new List<ShoppingListItem>()
                            {
                                new ShoppingListItem()
                                {
                                    Name = "Rice",
                                    Quantity = 400,
                                    Unit = "grams"
                                }
                            }
                        },
                        new ShoppingList()
                        {
                            Category = "Carbs",
                            Items = new List<ShoppingListItem>()
                            {
                                new ShoppingListItem()
                                {
                                    Name = "Rice",
                                    Quantity = 400,
                                    Unit = "grams"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Pasta",
                                    Quantity = 400,
                                    Unit = "grams"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Spinach",
                                    Quantity = 100,
                                    Unit = "grams"
                                }
                            }
                        },
                        new ShoppingList()
                        {
                            Category = "Carbs",
                            Items = new List<ShoppingListItem>()
                            {
                                new ShoppingListItem()
                                {
                                    Name = "Rice",
                                    Quantity = 400,
                                    Unit = "grams"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Pasta",
                                    Quantity = 400,
                                    Unit = "grams"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Spinach",
                                    Quantity = 2.5,
                                    Unit = "cups"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Eggs",
                                    Quantity = 2,
                                    Unit = "units"
                                },
                                new ShoppingListItem()
                                {
                                    Name = "Fruits",
                                    Quantity = 40,
                                    Unit = "dates"
                                }
                            }
                        },
                        new ShoppingList()
                        {
                            Category = "Carbs",
                            Items = new List<ShoppingListItem>()
                            {
                                new ShoppingListItem()
                                {
                                    Name = "Rice",
                                    Quantity = 400,
                                    Unit = "grams"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}