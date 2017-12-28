# ContextStausAPI

API to Manage Context Status on Lorien. Used to create, update, read and delete Context Status.

## Phase Product Data Format

These are the fields of the context status and it's constrains:

* context: Name of the Context of which the State is related to.
  * String (Up to 50 chars)
  * Mandatory
  * Primary Key
* contextDescription: Description of the Context of which the State is related to.
  * Integer
  * Mandatory
* statusName: Name of the Status on this context.
  * String (Up to 50 chars)
  * Mandatory
* value: Name of the Status on this context.
  * String (Up to 50 chars)
  * Mandatory
* startTimestampTicks: Start timestamp in ticks of the state.
  * Ignored
  * Automatically generated
* endTimestampTicks: Start timestamp in ticks of the state.
  * Ignored
  * Automatically generated

### JSON Example:

```json
{
  "context": "teste",
  "contextDescription": "Related to Test Data",
  "statusName": "Test 1",
  "value": "azul",
  "startTimestampTicks": 636500720021678964,
  "endTimestampTicks": 636500720986996539
}
```

# URLs

* api/contextstatus/{thingId}/{context}?{recurrent=false}

  * Get: Return Last state in that context of that thing
  * Put: Create or update the status of the context of a thing, if recurrent is true the same will be applyed to all it's children. Also saves de history on the database.

* api/contextstatus/history/{thingId}/{context}

  * Get: Return all the states in that context of that thing

# ThingStatusAPI

API to Manage Thing Status on Lorien. Used to create, update, read and delete Context Status.

## Phase Product Data Format

These are the fields of the Thing Status and it's constrains:

* thingId: Id of the Thing related to the status
  * Integer
  * Mandatory
* statusContexts: List of each status related to each context.
  * Context State JSON
  * Optional

### JSON Example:

```json
{
  "thingId": 1,
  "statusContexts": [
    {
      "context": "Teste",
      "contextDescription": "Related to Test Data",
      "statusName": "Test 1",
      "value": "azul"
    }
  ]
}
```

# URLs

* api/thingstatus/{thingId}?{recurrent=false}

  * Get: Return Last state of that thing
  * Put: Create or update the status of the thing of ALL CONTEXTS, if recurrent is true the same will be applyed to all it's children. Also saves de history on the database.

* api/contextstatus/history/{thingId}

  * Get: Return all the states in that thing

# GatewayAPI

API Responsible to provide access to information nedeed to compose the recipe
from other APIs

## URLs

* gateway/things/{id}

  * Get: Thing where thingId = ID
