--- users ---
CREATE TABLE public.users (
	id serial4 NOT NULL,
	username varchar(50) NOT NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT users_pkey PRIMARY KEY (id),
	CONSTRAINT users_username_key UNIQUE (username)
);

ALTER TABLE public.users OWNER TO "ualax-admin";
GRANT ALL ON TABLE public.users TO "ualax-admin";

--- tweets ---
CREATE TABLE public.tweets (
	id serial4 NOT NULL,
	user_id int4 NOT NULL,
	"content" varchar(140) NOT NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT tweets_pkey PRIMARY KEY (id),
	CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE
);
CREATE INDEX idx_tweet_created_at_id ON public.tweets USING btree (created_at DESC, id DESC);

ALTER TABLE public.tweets OWNER TO "ualax-admin";
GRANT ALL ON TABLE public.tweets TO "ualax-admin";

--- follows ---
CREATE TABLE public.follows (
	follower_id int4 NOT NULL,
	followed_id int4 NOT NULL,
	created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
	CONSTRAINT check_auto_follow CHECK ((follower_id <> followed_id)),
	CONSTRAINT follows_pkey PRIMARY KEY (follower_id, followed_id),
	CONSTRAINT fk_followed FOREIGN KEY (followed_id) REFERENCES public.users(id) ON DELETE CASCADE,
	CONSTRAINT fk_follower FOREIGN KEY (follower_id) REFERENCES public.users(id) ON DELETE CASCADE
);

ALTER TABLE public.follows OWNER TO "ualax-admin";
GRANT ALL ON TABLE public.follows TO "ualax-admin";

--- insert users ---
insert into public.users (username)
values ('fcastellitto'),
('uala'),
('testUser');

--- insert follows ---
insert into public.follows (follower_id, followed_id)
values (1, 2),
(1, 3),
(2, 1);

--- insert tweets ---
INSERT INTO public.tweets (user_id, content, created_at) VALUES
(1, 'Innovation distinguishes between a leader and a follower.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Your most unhappy customers are your greatest source of learning.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'When something is important enough, you do it even if the odds are not in your favor.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Stay hungry, stay foolish.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Don’t compare yourself with anyone in this world… if you do so, you are insulting yourself.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Persistence is very important. You should not give up unless you are forced to give up.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'The people who are crazy enough to think they can change the world are the ones who do.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'If you are born poor it’s not your mistake, but if you die poor it’s your mistake.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Failure is an option here. If things are not failing, you are not innovating enough.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Sometimes life is going to hit you in the head with a brick. Don’t lose faith.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'I choose a lazy person to do a hard job. Because a lazy person will find an easy way to do it.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'The first step is to establish that something is possible; then probability will occur.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Ideas are easy. Execution is everything.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Work like hell. I mean you just have to put in 80 to 100 hour weeks every week.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'The way to get started is to quit talking and begin doing.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'It’s fine to celebrate success, but it is more important to heed the lessons of failure.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'The biggest risk is not taking any risk.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Be so good they can’t ignore you.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'If something is important enough, you should try, even if the probable outcome is failure.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'The true sign of intelligence is not knowledge but imagination.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Do not be embarrassed by your failures, learn from them and start again.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Opportunities multiply as they are seized.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Success usually comes to those who are too busy to be looking for it.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'If you really look closely, most overnight successes took a long time.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Dream big, start small, act now.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Chase the vision, not the money; the money will end up following you.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'We cannot solve our problems with the same thinking we used when we created them.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'The only way to do great work is to love what you do.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Success is a lousy teacher. It seduces smart people into thinking they can’t lose.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'The future belongs to those who believe in the beauty of their dreams.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'You have to be burning with an idea, or a problem, or a wrong that you want to right.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Simplicity is the ultimate sophistication.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Do what you love and success will follow. Passion is the fuel behind a successful career.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Life is too short to spend it living someone else’s dream.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'The people who are crazy enough to think they can change the world are the ones who do.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'If you don’t build your dream, someone else will hire you to help them build theirs.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'I have not failed. I’ve just found 10,000 ways that won’t work.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'A person who never made a mistake never tried anything new.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Courage is being scared to death, but saddling up anyway.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'What would you do if you weren’t afraid?', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Act as if what you do makes a difference. It does.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'The way to get started is to quit talking and begin doing.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Doubt kills more dreams than failure ever will.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'Do what you can, with what you have, where you are.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(3, 'Opportunities don’t happen. You create them.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(1, 'Success is walking from failure to failure with no loss of enthusiasm.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180)),
(2, 'We are what we repeatedly do. Excellence, then, is not an act, but a habit.', NOW() - INTERVAL '1 day' * FLOOR(RANDOM() * 180));



