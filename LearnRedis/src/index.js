import { createClient } from "redis";

// Create redis client.
const client = createClient();

// check for any error.
client.on('error', (err) => {
  console.error("Redis client error", err);
});

// connect redis client.
await client.connect();

// store simple string into redis.
let response = await client.set('key', 'value');
console.log(response);

// retrive simple string.
const value = await client.get('key');
console.log(value);

// store a map 
const sessionResponse = await client.hSet('user-session:123', {
  name: "Rahul",
  surname: "Dey",
  company: "Redis",
  age: 24
});
console.log(sessionResponse);

// retrive a map 
let userSession = await client.hGetAll('user-session:123');
console.log(JSON.stringify(userSession, null, 2));

const fieldsAdded = await client.hSet('bike:1',
  {
    model: "Metor 350 Supernova Blue",
    brand: "Royal Enfield",
    type: "bs6 E20 bikes",
    price: 230000,
  },
);
console.log(`Number of fields were added: ${fieldsAdded}`);

const model = await client.hGet('bike:1', 'model');
console.log(`Model: ${model}`);
