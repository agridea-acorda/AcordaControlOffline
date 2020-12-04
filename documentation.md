# Documentation

### DDD and Aggregate Roots
DDD concepts are used to implement our domain logic. There are three aggregate roots (ARs): Inspection, Farm, Checklist. They may reference one another and thus persist the Id of the referenced AR (see below).

### Data store (in browser)
We use [localstorage](https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage) to store data locally, together with [Blazored/LocalStorage](https://github.com/blazored/LocalStorage).
Domain model objects are persisted using the keys Inspection_\{FarmInspectionId\}, Farm_\{FarmId\}, and Checklist_\{ChecklistId\}. Given that there is no relationnal concept with localstorage, which is just a basic key/string store,
we will need several [getItem](https://developer.mozilla.org/en-US/docs/Web/API/Storage/getItem) calls to read a complete aggregate root instance. But that's fine.

### Api calls
* api fetch mandate list to viewmodel json
* api fetch mandate detail to domain model json
* api save mandate (interface to be defined)
