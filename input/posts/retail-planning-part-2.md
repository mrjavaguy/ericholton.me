Title: Retail Planning Part 2
Published: 11/05/2017
Tags: 
  - C#
  - retail
  - planning
---
# A little deeper
We need to think about what the minimum viable change (MVC) for starting this application. I am tending toward the interaction between different planning variables in the same hierarchy.
## Sales Planning
Typically, there are three main variables used for sales planing. Last Year Sales, Expected Sales This Year, and Percent Change (LY vs TY). Last Year Sales (LY_SALES) should be *locked* as they can not be changed by the planner (normally some sort of ETL will get this data).  This year expected sales (TY_SALES) is defined as LY_SALES * (1 + SALES_PERCENT). Conversly, percent change (SALES_PERCENT) is defined as TY_SALES/LY_SALES -1. The person doing the planning need to be able to enter the number in either TY_SALES or SALES_PERCENT and have it update the other value.

### Class design
Based on the above we need to have something like

``` csharp
class PlanVariable
{
    private decimal value;
    private bool locked;
    private string name;

    public PlanVariable(string name, decimal currentValue, bool locked)
    {
        this.value = currentValue;
        this.locked = locked;
        this.name = name;
    }

    public decimal Value => value;
    public bool Locked => locked;
    public string Name => this.name;
}
```

We will also need to add in some eventing for letting other variable know when we have had are value changed. 
