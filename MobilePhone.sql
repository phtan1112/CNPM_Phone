create database MobilePhoneProduct
go
use MobilePhoneProduct
go
create table phone_group(
    id int primary key identity ,
    name varchar(255)
)
go
create table accountant(
    id int primary key identity,
	username varchar(250) not null,
	password varchar(20)
)
go
create table agent(
    id int primary key identity,
    agent_name varchar(250) not null,
	username  varchar(250)  not null,
	password varchar(20),
	address varchar(250),
	phone_number varchar(12),
	create_by_accountID int not null,
    FOREIGN KEY (create_by_accountID) references accountant(id)
)
go
create table phones(
	id int primary key identity,
	name varchar(250) not null,
	price float,
	quantity int,
	image varchar(255) default null,
	group_id int not null ,
	foreign key(group_id) references phone_group(id)
)

go
create table import(
	id int primary key identity,
	created_date date,
	created_by int not null, --  id of accountant
    foreign key(created_by) references accountant(id)
)
go

create table import_detail(
	id int primary key identity,
	import_id int,
	phone_id int,
	quantity int,
	import_price float default null,
	foreign key(import_id) references import(id),
	foreign key(phone_id) references phones(id)
)
go
create table agent_order(
	id int primary key identity,
	agent_id int not null,
	order_date date not null,
	total int,
	status_order bit not null, -- 1 la dang chuyen, 0 la dang xu li (đang chuyễn nghĩa là đã pay successful)
	status_pay bit not null, -- 1 la thanh toan roi, 0 la chua thanh toan,
	method_pay varchar(50),
	foreign key(agent_id) references agent(id)
)

go
create table agent_order_detail(
	id int primary key identity,
	order_id int not null,
	id_phone int not null,
	quantity int not null,
	foreign key(order_id) references agent_order(id),
	foreign key(id_phone) references phones(id)
)
go
create table sold(
	id int primary key identity,
	phone_id int not null,
	quantity int not null,
	foreign key(phone_id) references phones(id)
)
go

insert into phone_group(name) values ('Apple'),('Samsung'),('Xiaomi'),
                                     ('Oppo'),('Vivo'),('Nokia'),('Realme'),
                                     ('Oneplus'),('ASUS')
go
insert into accountant(username, password) values ('philong1','123456'),
                                                  ('520h0067','456789'),
                                                    ('user1','987654'),
                                                    ('user2','654321')
go
insert into agent(agent_name,username,address, password,phone_number,create_by_accountID)
                                            values ('CellPhoneS','cellphone11','SGN','123456',113,1),
                                                  ('FPT','fpt11','SGN','456789',114,2),
                                                  ('DiDongViet','didongviet11','SGN','123456',115,1)
go
insert into phones(name,quantity,price,group_id,image)
                values('Iphone 11 Pro max',10,12000000,1,'https://cdn.tgdd.vn/Products/Images/42/200533/iphone-11-pro-max-green-600x600.jpg'),  -- 1
                      ('Iphone 12 Pro max',7,19000000,1,'https://cdn2.cellphones.com.vn/358x358,webp,q100/media/catalog/product/1/_/1_251_1.jpg'),   -- 2
                      ('Iphone 13 Pro max',20,22000000,1,'https://www.apple.com/newsroom/images/product/iphone/standard/Apple_iPhone-13-Pro_iPhone-13-Pro-Max_09142021_inline.jpg.large.jpg'),  -- 3
                      ('Xiaomi 13 8GB 256GB',15,22000000,3,'https://cdn.tgdd.vn/Products/Images/42/267984/xiaomi-13-thumb-den-600x600.jpg'),    -- 4
                      ('Samsung galaxy S23 ultra 256GB',30,26990000,2,'https://cdn.tgdd.vn/Products/Images/42/249948/samsung-galaxy-s23-ultra-1-600x600.jpg'), -- 5
                      ('Samsung galaxy Z Flip4 128GB',8,19990000,2,'https://cdn.tgdd.vn/Products/Images/42/258047/samsung-galaxy-z-flip4-5g-128gb-thumb-tim-600x600.jpg'), -- 6
                      ('Xiaomi 12T',13,111490000,3,'https://cdn.tgdd.vn/Products/Images/42/279065/xiaomi-12t-thumb-600x600.jpg'),-- 7
                      ('Iphone XS Max 128GB',4,17490000,1,'https://product.hstatic.net/1000370129/product/iphone-xs-max-128gb-99pct-gia-bao-nhieu-den_4567b63955884b81a215eb6ea991e09f_master.jpg')  -- 8
                        -- 8 phones with 8 id


go
insert into import(created_date,created_by) values('2022-2-10',1),('2022-2-11',1), -- year-day-month
                                       ('2022-2-12',1),('2023-2-1',2)
										,('2023-2-2',2),('2023-2-3',2) -- 6 imports
go
insert into import_detail(import_id,phone_id,quantity) values(1,1,8),(1,2,5),(1,3,5),
                                                             (2,3,5),(2,4,5),(2,5,7),
                                                             (3,3,5),(3,7,5),(3,4,5),
                                                             (4,6,4),(4,7,8),(4,8,2),
                                                             (5,4,5),(5,5,23),(5,6,4),
                                                             (6,3,5),(6,2,2),(6,1,2)
-- if price is null, và giá nó vẫn như  cũ
go
insert into agent_order(agent_id,order_date,total,status_order,status_pay,method_pay)
                                            values(1,'2022-05-3',477460000,1,1,'Banking'),
                                                  (2,'2022-05-9',500440000,0,1,'Momo'),
                                                  (1,'2021-05-3',795860000,1,0,'Cash'),
                                                  (2,'2021-08-3',240430000,0,0,'Cash'),
                                                  (2,'2022-05-3',240430000,0,0,'Banking'),
                                                  (1,'2022-07-3',240430000,1,0,'Cash'),
                                                  (2,'2022-05-3',240430000,0,1,'Banking'),
                                                  (1,'2022-05-3',284430000,0,0,'Momo'),
                                                  (3,'2022-07-3',240430000,0,0,'Cash'),
                                                  (3,'2022-05-3',196430000,0,1,'Banking'),
                                                  (3,'2022-05-3',240430000,0,0,'Momo')
go
insert into agent_order_detail(order_id ,id_phone,quantity)
	values (1,2,2),(1,1,1),(1,4,3),(1,5,1),(1,7,3),
			(2,1,5),(2,3,2),(2,5,1),(2,7,3),(2,8,2),
			(3,5,3),(3,2,1),(3,3,5),(3,6,7),(3,7,4),
			(4,1,3),(4,3,2),(4,5,4),(4,8,1),(4,8,2),
			(5,1,3),(5,3,2),(5,5,4),(5,8,1),(5,8,2),
			(6,1,3),(6,3,2),(6,5,4),(6,8,1),(6,8,2),
			(7,1,3),(7,3,2),(7,5,4),(7,8,1),(7,8,2),
			(8,1,3),(8,3,2),(8,5,4),(8,8,1),(8,8,2),
			(9,1,3),(9,3,2),(9,5,4),(9,8,1),(9,8,2),
			(10,1,3),(8,3,2),(10,5,4),(10,8,1),(10,8,2),
			(11,1,3),(11,3,2),(11,5,4),(11,8,1),(11,8,2)
go
insert into sold(phone_id,quantity) values(1,6),(2,3),(3,9),
                                            (4,3),(5,9),(6,7),(7,10),(8,3)
go


---- store procedure (With sql server)
-- xem lại thống kê hàng vào

CREATE PROCEDURE goods_received
AS
    BEGIN
        select p.id, p.name,p.quantity, sum(id.quantity) as 'number of phone import'
        from import_detail id, phones p
        where id.phone_id = p.id
        group by  p.id, p.name,p.quantity;
    END
go

 --  hàng ra,
CREATE PROCEDURE goods_sold
AS
    BEGIN
        select p.id, p.name,p.quantity, sum(s.quantity) as 'number of phones sold'
        from sold s,phones p
        where p.id = s.phone_id
        group by p.id, p.name,p.quantity;
    END
go
 --  mặt hàng bán chạy nhất
CREATE PROCEDURE best_selling_goods
AS
    BEGIN
        select p.id, p.name,p.price, sum(s.quantity) as 'number of best selling products'
        from sold s,phones p
        where p.id = s.phone_id
        group by p.id, p.name,p.price
        having sum(s.quantity) = (select  max(max_sell.sum_quantity) as 'best_selling_product'
                          from (select p.id, p.name,p.quantity, sum(s.quantity) as sum_quantity
                                from sold s,phones p
                                where p.id = s.phone_id
                                group by  p.id, p.name,p.quantity) as max_sell)
    END
go
--  doanh thu của từng tháng
CREATE PROCEDURE revenue_report_monthly
AS
    BEGIN
        select o.order_date,sum(od.quantity*p.price) as 'revenue monthly'
        from agent_order o,agent_order_detail od,phones p
        where o.id = od.order_id and p.id = od.id_phone
        group by o.order_date;
    END
go

select id,agent_name,address,phone_number from agent where username = 'fpt11' and password = 456789

select * from phone_group
select * from accountant
select * from agent
select * from phones
select * from import
select * from import_detail
select * from agent_order
select * from agent_order_detail
select * from sold
go

EXEC goods_received
go

EXEC goods_sold
go

EXEC best_selling_goods
go

EXEC revenue_report_monthly