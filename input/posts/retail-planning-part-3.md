Title: Retail Planning Part 3
Published: 11/12/2017
Tags: 
  - C#
  - retail
  - planning
---
# Code Avaialble
The source for this project is availible now on [GitHub](https://github.com/mrjavaguy/retailplanner)

## Testing Unit
We are going to use TDD/BDD for testing this project as we are building it. Here are some of the basic tests for the `PlanVariable` class. As you can see from the unit test class below, I have decided to use [xUnit](https://xunit.github.io/).

```csharp
    public class PlanVariableTests
    {
        [Fact]
        public void LockedVariableValueDoesNotChange()
        {
            var variable = new PlanVariable(string.Empty, 100, true);
            var attempt = variable.Update(200);

            Assert.Same(variable, attempt);
        }

        [Fact]
        public void UnlockedVariableValueDoesChange()
        {
            const decimal newValue = 200;
            var variable = new PlanVariable(string.Empty, 100, false);
            var attempt = variable.Update(newValue);

            Assert.Equal(newValue, attempt.Value);
            Assert.NotSame(variable, attempt);
        }

        [Fact]
        public void StartWithUnlockAndThenLock()
        {
            const decimal newValue = 200;
            var variable = new PlanVariable(string.Empty, 100, false);
            var locked = variable.Lock();
            var attempt = locked.Update(newValue);

            Assert.Equal(variable.Value, locked.Value);
            Assert.Same(locked, attempt);
        }

        [Fact]
        public void StartWithLockAndThenUnlock()
        {
            const decimal newValue = 200;
            var variable = new PlanVariable(string.Empty, 100, true);
            var unlocked = variable.Unlock();
            var attempt = unlocked.Update(newValue);

            Assert.Equal(newValue, attempt.Value);
            Assert.NotSame(unlocked, attempt);
        }
    }
```

# Acceptence Testing
For acceptence testing, we are going to use [StoryTeller](http://storyteller.github.io/). For now until we get more code, the tests look similar to the unit tests.

``` csharp
    public class PlanVariableFixture : Fixture
    {
        public PlanVariableFixture()
        {
            Title = "Plan Variable Specification";
        }

        private PlanVariable variable;

        [FormatAs("Starting with a locked variable with {startValue}")]
        public void LockedPlanVariable(decimal startValue)
        {
            variable = new PlanVariable("spec", startValue, true);
        }

        [FormatAs("Starting with a unlocked variable with {startValue}")]
        public void UnlockedPlanVariable(decimal startValue)
        {
            variable = new PlanVariable("spec", startValue, false);
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
    }
```

The results of the acceptence tests 

![StoryTellerPlanVariable](/assets/images/StoryTellerPlanVariable.png)