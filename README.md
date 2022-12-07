# SmartMapper
An object-to-object mapper. Separated into four mapping categories: adapters, mappers, selectors, and specifiers.

**Adapters** map the property values of a source object to a new target object, where a new instance of the target object is created.
<br/>
The properties of the source object can be mapped as parameters of a constructor of the target object, as method parameters, or to other properties of the target object.


**Mappers** are similar to adapters, but the target object must exist, a new object is not instantiated.
<br/>
So the source object's properties are not mapped as constructor parameters, but are mapped as method parameters and to other properties.


**Selectors** read values from source object properties to assign to a target object's properties, where a new instance of the target object is always created.
<br/>
The selectors may look like adapters, but there are big differences.
<br/>
First is that selectors start from the properties of the target object to know which property of the source object to read from. This is the reverse of what happens with adapters and mappers.
<br/>
Second, selectors can only assign properties, you cannot map them to constructors or methods.

**Specifiers** are different from the previous ones. Instead of assigning values, they apply conditions on queries.
<br/>
The source of the selectors are filter objects, and the target is queryable models, such as entities.
<br/>
Instead of assigning values between properties, specifiers apply the **Where** condition of an **IQueryable**, comparing the values of the filter (source) and the entity (target).

## Stats

![Alt](https://repobeats.axiom.co/api/embed/7a40a95757f9d974e0fcbf4348c9615f6004cf15.svg "Repobeats analytics image")
