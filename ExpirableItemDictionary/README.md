## ExpirableItemDictionary

This was a dictionary implementation that automatically removed items given a
timespan. It was not threaded, so the self-maintenance logic of expiring
dictionary items would occur while dictionary retrieval and insert methods
are invoked.

For more information, see my blog post:

http://www.jondavis.net/techblog/post/2010/08/30/Four-Methods-Of-Simple-Caching-In-NET.aspx

