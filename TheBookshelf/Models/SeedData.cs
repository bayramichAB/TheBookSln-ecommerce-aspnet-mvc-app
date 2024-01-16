using Microsoft.EntityFrameworkCore;

namespace TheBookshelf.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (context.Books.Count() == 0 && context.Categories.Count() == 0 && context.Authors.Count() == 0)
            {
                Author A1 = new Author { Name = "Anna Reid", Biography = "Anna Reid (born 1965) is an English journalist and author whose work focuses primarily on the history of Eastern Europe" };
                Author A2 = new Author { Name = "Arthur Doyle", Biography = "Sir Arthur Conan Doyle was born in Edinburgh in 1859 and died in 1930. Within those years was crowded a variety of activity and creative work that made him an international figure and inspired the French to give him the epithet 'the good giant'. He was the nephew of 'Dickie Doyle' the artist, and was educated at Stonyhurst, and later studied medicine at Edinburgh University, where the methods of diagnosis of one of the professors provided the idea for the methods of deduction used by Sherlock Holmes." };
                Author A3 = new Author { Name = "Robert T. Kiyosaki", Biography = "Robert Kiyosaki, author of Rich Dad Poor Dad - the international runaway bestseller that has held a top spot on the New York Times bestsellers list for over six years - is an investor, entrepreneur and educator whose perspectives on money and investing fly in the face of conventional wisdom. He has, virtually single-handedly, challenged and changed the way tens of millions, around the world, think about money." };
                Author A4 = new Author { Name = " Itzik Ben-Gan", Biography = "Itzik Ben-Gan is an independent T-SQL Trainer. A Microsoft Data Platform MVP (Most Valuable Professional) since 1999, Itzik has delivered numerous training events around the world focused on T-SQL Querying, Query Tuning and Programming. Itzik is the author of several books including T-SQL Fundamentals, T-SQL Querying and T-SQL Window Functions. He has written articles for red-gate.com/simple-talk, sqlperformance.com, ITPro Today and SQL Server Magazine. Itzik’s speaking activities include PASS Data Community Summit, SQLBits, and various events and user groups around the world. Itzik developed and is regularly delivering his Advanced T-SQL Querying, Programming and Tuning and T-SQL Fundamentals courses." };
                Author A5 = new Author { Name = "Simon Proudman", Biography = "Simon Proudman is an Australian travel blogger whose passion lies with far flung places, so that’s what he named his blog! From Papua New Guinea to Turkmenistan, Simon has seen parts of the globe that many of us have only heard of." };
                Author A6 = new Author { Name = "Leo Tolstoy", Biography= "Lev Nikolayevich Tolstoy (Russian: Лев Николаевич Толстой; most appropriately used Liev Tolstoy; commonly Leo Tolstoy in Anglophone countries) was a Russian writer who primarily wrote novels and short stories. Later in life, he also wrote plays and essays. His two most famous works, the novels War and Peace and Anna Karenina, are acknowledged as two of the greatest novels of all time and a pinnacle of realist fiction. Many consider Tolstoy to have been one of the world's greatest novelists. Tolstoy is equally known for his complicated and paradoxical persona and for his extreme moralistic and ascetic views, which he adopted after a moral crisis and spiritual awakening in the 1870s, after which he also became noted as a moral thinker and social reformer." };
                Author A7 = new Author { Name = "Mary Shelley", Biography = "Mary Shelley (1797-1851), the only daughter of writers William Godwin and Mary Wollstonecraft, and wife of Percy Bysshe Shelley, is the critically acclaimed author of Frankenstein, Valperga, and The Last Man, in addition to many other works. Mary Shelley s writings reflect and were influenced by a number of literary traditions including Gothic and Romantic ideals, and Frankenstein is widely regarded as the first modern work of science fiction" };
                Author A8 = new Author { Name = "Phil Knight", Biography= "Phil Knight (born February 24, 1938, Portland, Oregon, U.S.) American businessman who cofounded (1964) the multinational sportswear and sports equipment corporation Nike, Inc. (originally called Blue Ribbon Sports). During his tenure as CEO (1964–2004), Nike became one of the most successful companies in the world." };
                Author A9 = new Author { Name = "Megan Miller", Biography = "Writer of best-selling books about Minecraft" };


                Category c1 = new Category { Name = "History" };
                Category c2 = new Category { Name = "Literature" };
                Category c3 = new Category { Name = "Business" };
                Category c4 = new Category { Name = "Computer & Technology" };
                Category c5 = new Category { Name = "Travel" };

                context.Books.AddRange(
                    new Book
                    {
                        Name = "Leningrad: Tragedy of a City Under Siege, 1941-44",
                        Price = 10.76m,
                        Available = true,
                        Pages = "492",
                        Date = new DateTime(2011,1, 1),
                        Description = "On 8 September 1941, 11 weeks after Hitler launched Operation Barbarossa, Leningrad was surrounded. The siege would not end for 2 and a half years and as many as 2 million Soviet lives would be lost. This narrative history is interwoven with personal stories of daily siege life drawn from diarists and memoirists on both sides.",
                        img= "/img/Leningrad.jpg",
                        Category = c1,
                        Author = A1

                    },
                    new Book
                    {
                        Name = "The Memoirs of Sherlock Holmes",
                        Price = 17.31m,
                        Available = true,
                        Pages = "302",
                        Date = new DateTime(2023,8,24),
                        Description = "\"The Memoirs of Sherlock Holmes\" takes readers on a gripping journey through a collection of intriguing cases solved by the brilliant detective, Sherlock Holmes, and his loyal companion, Dr. Watson. In the first thrilling tale, \"Silver Blaze,\" Holmes and Watson are called to Dartmoor to investigate the mysterious disappearance of the favorite racehorse for the Wessex Cup and the murder of its trainer. As they unravel the clues and confront suspicious characters, they find themselves drawn deeper into a web of deception and danger.",
                        img = "/img/SherlockHolmes.jpg",
                        Category = c2,
                        Author = A2

                    },
                    new Book
                    {
                        Name = "Rich Dad Poor Dad",
                        Price = 10.99m,
                        Available = true,
                        Pages = "336",
                        Date = new DateTime(2022,4,5),
                        Description = "April of 2022 marks a 25-year milestone for the personal finance classic Rich Dad Poor Dad that still ranks as the #1 Personal Finance book of all time. And although 25 years have passed since Rich Dad Poor Dad was first published, readers will find that very little in the book itself has changed — and for good reason. While so much in our world is changing a high speed, the lessons about money and the principles of Rich Dad Poor Dad haven’t changed. Today, as money continues to play a key role in our daily lives, the messages in Robert Kiyosaki’s international bestseller are more timely and more important than ever.",
                        img = "/img/RichDad.jpg",
                        Category = c3,
                        Author = A3

                    },
                    new Book
                    {
                        Name = "Microsoft SQL Server 2012 T-SQL Fundamentals",
                        Price = 19m,
                        Available = true,
                        Pages = "401",
                        Date = new DateTime(2012,7,15),
                        Description = "Master the fundamentals of Transact-SQL—and develop your own code for querying and modifying data in Microsoft® SQL Server® 2012. Led by a SQL Server expert, you’ll learn the concepts behind T-SQL querying and programming, and then apply your knowledge with exercises in each chapter. Once you understand the logic behind T-SQL, you’ll quickly learn how to write effective code—whether you’re a programmer or database administrator.",
                        img = "/img/SqlServer.jpg",
                        Category = c4,
                        Author = A4

                    },
                    new Book
                    {
                        Name = "Turkmenistan: Far Flung Places Travel Guide",
                        Price = 9.95m,
                        Available = true,
                        Pages = "262",
                        Date = new DateTime(2017,5,13),
                        Description = "Turkmenistan, a country once closed to visitors during its time as a Soviet republic, is now attracting more intrepid tourists. Beautiful ancient Silk Road cities contrast with striking marble-clad modern architecture and a massive gas crater burning in the middle of the desert. There is a lot to see and enjoy in this unusual central Asian destination.\r\n\r\nDetailed information of the cities and attractions with maps and invaluable contact information. Learn how to travel around and find the best places to visit, stay and eat.",
                        img = "/img/Turkmenistan.jpg",
                        Category = c5,
                        Author = A5

                    },
                    new Book
                    {
                        Name = "War and Peace (Vintage Classics)",
                        Price = 12.44m,
                        Available = true,
                        Pages = "1296",
                        Date = new DateTime(2008,12,2),
                        Description = "War and Peacebroadly focuses on Napoleon’s invasion of Russia in 1812 and follows three of the most well-known characters in literature: Pierre Bezukhov, the illegitimate son of a count who is fighting for his inheritance and yearning for spiritual fulfillment; Prince Andrei Bolkonsky, who leaves his family behind to fight in the war against Napoleon; and Natasha Rostov, the beautiful young daughter of a nobleman who intrigues both men.",
                        img = "/img/WarAndPeace.jpg",
                        Category = c2,
                        Author = A6

                    },
                    new Book
                    {
                        Name = "Frankenstein",
                        Price = 6.73m,
                        Available = false,
                        Pages = "144",
                        Date =new DateTime(2020,12,21),
                        Description = "Frankenstein; or, The Modern Prometheus, is a novel written by English author Mary Shelley about the young student of science Victor Frankenstein, who creates a grotesque but sentient creature in an unorthodox scientific experiment. Shelley started writing the story when she was eighteen, and the novel was published when she was twenty. The first edition was published anonymously in London in 1818. Shelley's name appears on the second edition, published in France in 1823.Shelley had travelled through Europe in 1814, journeying along the river Rhine in Germany with a stop in Gernsheim which is just 17 km (10 mi) away from Frankenstein Castle, where two centuries before an alchemist was engaged in experiments",
                        img = "/img/Frankenstein.jpg",
                        Category = c2,
                        Author = A7

                    },
                    new Book
                    {
                        Name = "Shoe Dog: A Memoir by the Creator of Nike",
                        Price = 12.60m,
                        Available = true,
                        Pages = "512",
                        Date = new DateTime(2016,4,26),
                        Description = "In this candid and riveting memoir, for the first time ever, Nike founder and CEO Phil Knight shares the inside story of the company's early days as an intrepid start-up and its evolution into one of the world's most iconic, game-changing, and profitable brands.",
                        img = "/img/Nike.jpg",
                        Category = c3,
                        Author = A8

                    },
                    new Book
                    {
                        Name = "The Ultimate Unofficial Encyclopedia for Minecrafters",
                        Price = 17.45m,
                        Available = false,
                        Pages = "176",
                        Date = new DateTime(2015,6,16),
                        Description = "By New York Times bestselling author and Minecraft expert, Megan Miller, a full-color book full of practical advice that boys and girls will refer to again and again!",
                        img = "/img/Minecraft.jpg",
                        Category = c4,
                        Author = A9

                    },
                    new Book
                    {
                        Name = "T-SQL Querying (Developer Reference) 1st Edition, Kindle Edition",
                        Price = 35.99m,
                        Available = true,
                        Pages = "2974",
                        Date = new DateTime(2015,02,17),
                        Description = "T-SQL insiders help you tackle your toughest queries and query-tuning problems\r\nSqueeze maximum performance and efficiency from every T-SQL query you write or tune. Four leading experts take an in-depth look at T-SQL’s internal architecture and offer advanced practical techniques for optimizing response time and resource usage. Emphasizing a correct understanding of the language and its foundations, the authors present unique solutions they have spent years developing and refining. All code and techniques are fully updated to reflect new T-SQL enhancements in Microsoft SQL Server 2014 and SQL Server 2012.",
                        img = "/img/T-SQLQuerying.jpg",
                        Category = c4,
                        Author = A4

                    });
                context.SaveChanges();
            }
        }
    }
}
