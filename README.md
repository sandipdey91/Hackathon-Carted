Inspiration
When shopping on Amazon, I've been noticing that it's hard to forget to add a needed item to my order. 
The website suggests similar items, "also bought with" items, and other items I may be interested in. 
What if it was that easy to avoid forgetting something on an everyday trip to the grocery store?

What it does
Some large grocery stores around the world are adding screen-based advertisement mounted on the cart itself. 
Carted takes this concept a step further, with both front and rear view cameras. 
Carted employs facial recognition to recall past purchase history and combines that information with the contents of your cart in real time. 
For example, if you have spaghetti noodles, marinara sauce, and french bread in your cart and are headed to the checkout, Carted may then ask if you forgot to buy parmesan. 
It then can also suggest new items, or alert the customer about sales on items they usually buy. 
Imagine how helpful this could be to the busy mom who has absolutely no time to make a second trip to the grocery store!

How we built it
We achieved this by implementing facial recognition on open cv using Python, and object recognition was added using Google cloud platform ML APIs. 
The user interface was built using angular.js and angolia.

Challenges we ran into
It took time to train the facial recognition model. 
Additionally, training the model to accurately predict related items after scanning your product in the cart proved to be a challenge.

Accomplishments that we're proud of
We trained our model rigorously with 218 faces to achieve the most accurate prediction possible on this scale.

What we learned
We have learned so many things with this project - creativity, effective teamwork, working efficiently under a deadline, and practical application of facial recognition and Google ML APIs.

What's next for Carted
We plan to improve our model and continue to make it more reliable and accurate, adding extensive security and privacy features, and also implementing a new GPS feature which will guide the customer toward the location of the desired product in the store.
