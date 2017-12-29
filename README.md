# BikeTheT

An API which you can use to make note of gift ideas throughout the year - to make buying gifts much easier!

Basic CRUD functionality on all resources is supported. Authentication uses RapidAPI headers.

## Holidays

```
POST /api/v1/holidays
{
  "name": "Independence Day",
  "month": 7,
  "day": 4
}
```

## Recipients

```
POST /api/v1/recipients
{
  "name": "Alice"
}
```

## GiftIdeas

```
POST /api/v1/giftIdeas
{
  "holidayId": 123,
  "recipientId": 456,
  "giftDescription": "books"
}
```

The `giftIdeas` collection may be optionally queried by holiday or recipient:

```
GET /api/v1/giftIdeas?holidayId=123&recipientId=456

200 OK
{
  "items": [
    {
      "holidayId": 123,
      "recipientId": 456,
      "giftDescription": "books"
    }  
  ]
}
```
