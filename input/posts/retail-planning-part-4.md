Title: Retail Planning Part 4
Published: 11/15/2017
Tags: 
  - C#
  - retail
  - planning
  - unit testing
  - acceptance testing
---
# Eventing
Each of the plan variable will raise an event when it value changes. At this point I have only done the simplest thing possible, however I know that in the future (based on upcoming stories) that it will be more complex.

## New Unit Test
```csharp
        public void EventHappensOnChangedValue()
        {
            var variable = new PlanVariable(string.Empty, 100, true);
            var eventHappened = false;
            variable.SubScribe(() => eventHappened = true);
            Assert.True(eventHappened);
        }
```

## Update Spec Fixture
```csharp
    public class PlanVariableFixture : Fixture
    {
        public PlanVariableFixture()
        {
            Title = "Plan Variable Specification";
        }

        private PlanVariable variable;
        private bool eventSubscription = false;


        [FormatAs("Starting with a locked variable with {startValue}")]
        public void LockedPlanVariable(decimal startValue)
        {
            variable = new PlanVariable("spec", startValue, true);
        }

        [FormatAs("Starting with a unlocked variable with {startValue}")]
        public void UnlockedPlanVariable(decimal startValue)
        {
            variable = new PlanVariable("spec", startValue, false);
            variable.SubScribe(() => eventSubscription = true);
        }

        [FormatAs("Change the value {value}")]
        public void ChangeTheValue(decimal value)
        {
            variable = variable.Update(value);
        }

        [FormatAs("The value should be {value}")]
        public decimal TheValueShouldBe()
        {
            return variable.Value;
        }

        public bool AndEventIsRaised()
        {
            return eventSubscription;
        }
    }
```

## Specification as written
```md
# Unlocked Plan Variable value change will raise an event

-> id = db3bb374-0c72-4104-a304-78bcca5e5214
-> lifecycle = Acceptance
-> max-retries = 0
-> last-updated = 2017-11-13T00:56:48.4768642Z
-> tags = 

[PlanVariable]
|> UnlockedPlanVariable startValue=123.45
|> ChangeTheValue value=234.56
~~~
```