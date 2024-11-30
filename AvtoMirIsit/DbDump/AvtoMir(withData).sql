--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2024-11-30 14:35:51

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 16766)
-- Name: Автомобиль; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Автомобиль" (
    "id_автомобиля" integer NOT NULL,
    "Номер" character varying(20),
    "vin_номер" character varying(20),
    "Год_выпуска" character varying(4),
    "Цена" integer,
    "Цвет" character varying(20),
    "id_типа" integer,
    photo character varying(300)
);


ALTER TABLE public."Автомобиль" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16769)
-- Name: Договор; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Договор" (
    "id_договора" integer NOT NULL,
    "Дата_продажи" date,
    "Сумма_продажи" integer,
    "id_сотрудника" integer,
    "id_автомобиля" integer,
    "id_клиента" integer
);


ALTER TABLE public."Договор" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16772)
-- Name: Клиент; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Клиент" (
    "id_клиента" integer NOT NULL,
    "ФИО" character varying(50),
    "Адрес" character varying(50),
    "Телефон" character varying(20)
);


ALTER TABLE public."Клиент" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16775)
-- Name: Магазин; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Магазин" (
    "id_магазина" integer NOT NULL,
    "Адрес" character varying(50)
);


ALTER TABLE public."Магазин" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16778)
-- Name: МаркаАвтомобиля; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."МаркаАвтомобиля" (
    "id_марка" integer NOT NULL,
    "Марка" character varying(30)
);


ALTER TABLE public."МаркаАвтомобиля" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16781)
-- Name: Сотрудник; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Сотрудник" (
    "id_сотрудника" integer NOT NULL,
    "ФИО" character varying(50) NOT NULL,
    "Телефон" character varying(20) NOT NULL,
    "id_магазина" integer NOT NULL
);


ALTER TABLE public."Сотрудник" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16784)
-- Name: ТипАвтомобиля; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ТипАвтомобиля" (
    "id_типа" integer NOT NULL,
    "Модель" character varying(30),
    "id_марка" integer
);


ALTER TABLE public."ТипАвтомобиля" OWNER TO postgres;

--
-- TOC entry 4875 (class 0 OID 16766)
-- Dependencies: 215
-- Data for Name: Автомобиль; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Автомобиль" ("id_автомобиля", "Номер", "vin_номер", "Год_выпуска", "Цена", "Цвет", "id_типа", photo) VALUES (1, 'E102CH161', 'JHMCM56557C404453', '2000', 2000000, 'Grey', 1, 'https://auto4travel.com/wp-content/uploads/2020/07/-%D0%A1%D0%B5%D1%80%D1%8B%D0%B9-1-1-e1707828333611.jpg');
INSERT INTO public."Автомобиль" ("id_автомобиля", "Номер", "vin_номер", "Год_выпуска", "Цена", "Цвет", "id_типа", photo) VALUES (3, 'У507УК76', 'JHMCM56557C404412', '2020', 6768678, 'желтый', 2, 'https://i0.shbdn.com/photos/54/09/30/x5_11705409306bi.jpg');
INSERT INTO public."Автомобиль" ("id_автомобиля", "Номер", "vin_номер", "Год_выпуска", "Цена", "Цвет", "id_типа", photo) VALUES (4, 'С938ЗМ76', 'JHMCM56923C404445', '2010', 378975, 'Серый', 1, 'https://auto4travel.com/wp-content/uploads/2020/07/-%D0%A1%D0%B5%D1%80%D1%8B%D0%B9-1-1-e1707828333611.jpg');
INSERT INTO public."Автомобиль" ("id_автомобиля", "Номер", "vin_номер", "Год_выпуска", "Цена", "Цвет", "id_типа", photo) VALUES (2, 'E304УК76', 'JHMCM56557C404445', '2001', 234559, 'Серый', 1, 'https://simferopol.altera-auto.ru/upload/iblock/cdb/cdbfee67a8174c20ff51215fa526ed29.png');


--
-- TOC entry 4876 (class 0 OID 16769)
-- Dependencies: 216
-- Data for Name: Договор; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Договор" ("id_договора", "Дата_продажи", "Сумма_продажи", "id_сотрудника", "id_автомобиля", "id_клиента") VALUES (2, '2024-11-25', 23455, 3, 2, 10);
INSERT INTO public."Договор" ("id_договора", "Дата_продажи", "Сумма_продажи", "id_сотрудника", "id_автомобиля", "id_клиента") VALUES (3, '2024-11-25', 678768678, 3, 3, 2);
INSERT INTO public."Договор" ("id_договора", "Дата_продажи", "Сумма_продажи", "id_сотрудника", "id_автомобиля", "id_клиента") VALUES (4, '2024-11-25', 678768678, 3, 3, 2);
INSERT INTO public."Договор" ("id_договора", "Дата_продажи", "Сумма_продажи", "id_сотрудника", "id_автомобиля", "id_клиента") VALUES (5, '2024-11-26', 2000000, 4, 1, 4);


--
-- TOC entry 4877 (class 0 OID 16772)
-- Dependencies: 217
-- Data for Name: Клиент; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Клиент" ("id_клиента", "ФИО", "Адрес", "Телефон") VALUES (3, 'Боратов Анатолий Владимирович', 'Садовая 7', '+79865432409');
INSERT INTO public."Клиент" ("id_клиента", "ФИО", "Адрес", "Телефон") VALUES (4, 'Туров Андрей Викторович', 'Садовая 5', '+7 890 654 37 89');
INSERT INTO public."Клиент" ("id_клиента", "ФИО", "Адрес", "Телефон") VALUES (1, 'Лукин Александр Валерьевич', 'Уральская 7', '+7 965 654 74 32');
INSERT INTO public."Клиент" ("id_клиента", "ФИО", "Адрес", "Телефон") VALUES (2, 'Поляков Виктор Иванович', 'Полярная 25', '+7 945 456 12 45');
INSERT INTO public."Клиент" ("id_клиента", "ФИО", "Адрес", "Телефон") VALUES (10, 'Бережной Сергей Владимирович', 'Красноармейская 98', '+7 983 041 56 78');


--
-- TOC entry 4878 (class 0 OID 16775)
-- Dependencies: 218
-- Data for Name: Магазин; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Магазин" ("id_магазина", "Адрес") VALUES (1, 'Большая Морская ул., 18');
INSERT INTO public."Магазин" ("id_магазина", "Адрес") VALUES (2, 'Вознесенский просп., 44-46');


--
-- TOC entry 4879 (class 0 OID 16778)
-- Dependencies: 219
-- Data for Name: МаркаАвтомобиля; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."МаркаАвтомобиля" ("id_марка", "Марка") VALUES (1, 'Huyndai');
INSERT INTO public."МаркаАвтомобиля" ("id_марка", "Марка") VALUES (2, 'Chevrolet');


--
-- TOC entry 4880 (class 0 OID 16781)
-- Dependencies: 220
-- Data for Name: Сотрудник; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Сотрудник" ("id_сотрудника", "ФИО", "Телефон", "id_магазина") VALUES (4, 'Никита Коваленко Андреевич', '+7 903 674 55 33', 1);
INSERT INTO public."Сотрудник" ("id_сотрудника", "ФИО", "Телефон", "id_магазина") VALUES (2, 'Гавриленко Елена Викторовна', '+7 956 365 45 72', 2);
INSERT INTO public."Сотрудник" ("id_сотрудника", "ФИО", "Телефон", "id_магазина") VALUES (3, 'Дизель Вин Константинович', '+7 981 254 96 37', 1);


--
-- TOC entry 4881 (class 0 OID 16784)
-- Dependencies: 221
-- Data for Name: ТипАвтомобиля; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."ТипАвтомобиля" ("id_типа", "Модель", "id_марка") VALUES (1, 'Solaris', 1);
INSERT INTO public."ТипАвтомобиля" ("id_типа", "Модель", "id_марка") VALUES (2, 'Camaro', 2);


--
-- TOC entry 4713 (class 2606 OID 16788)
-- Name: Автомобиль Автомобиль_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Автомобиль"
    ADD CONSTRAINT "Автомобиль_pkey" PRIMARY KEY ("id_автомобиля");


--
-- TOC entry 4715 (class 2606 OID 16790)
-- Name: Договор Договор_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT "Договор_pkey" PRIMARY KEY ("id_договора");


--
-- TOC entry 4717 (class 2606 OID 16792)
-- Name: Клиент Клиент_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Клиент"
    ADD CONSTRAINT "Клиент_pkey" PRIMARY KEY ("id_клиента");


--
-- TOC entry 4719 (class 2606 OID 16794)
-- Name: Магазин Магазин_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Магазин"
    ADD CONSTRAINT "Магазин_pkey" PRIMARY KEY ("id_магазина");


--
-- TOC entry 4721 (class 2606 OID 16796)
-- Name: МаркаАвтомобиля МаркаАвтомобиля_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."МаркаАвтомобиля"
    ADD CONSTRAINT "МаркаАвтомобиля_pkey" PRIMARY KEY ("id_марка");


--
-- TOC entry 4723 (class 2606 OID 16798)
-- Name: Сотрудник Сотрудник_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Сотрудник"
    ADD CONSTRAINT "Сотрудник_pkey" PRIMARY KEY ("id_сотрудника");


--
-- TOC entry 4725 (class 2606 OID 16800)
-- Name: ТипАвтомобиля ТипАвтомобиля_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ТипАвтомобиля"
    ADD CONSTRAINT "ТипАвтомобиля_pkey" PRIMARY KEY ("id_типа");


--
-- TOC entry 4727 (class 2606 OID 16801)
-- Name: Договор r_3; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT r_3 FOREIGN KEY ("id_сотрудника") REFERENCES public."Сотрудник"("id_сотрудника");


--
-- TOC entry 4728 (class 2606 OID 16806)
-- Name: Договор r_4; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT r_4 FOREIGN KEY ("id_автомобиля") REFERENCES public."Автомобиль"("id_автомобиля");


--
-- TOC entry 4729 (class 2606 OID 16811)
-- Name: Договор r_5; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT r_5 FOREIGN KEY ("id_клиента") REFERENCES public."Клиент"("id_клиента");


--
-- TOC entry 4731 (class 2606 OID 16816)
-- Name: ТипАвтомобиля r_6; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ТипАвтомобиля"
    ADD CONSTRAINT r_6 FOREIGN KEY ("id_марка") REFERENCES public."МаркаАвтомобиля"("id_марка");


--
-- TOC entry 4726 (class 2606 OID 16821)
-- Name: Автомобиль r_7; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Автомобиль"
    ADD CONSTRAINT r_7 FOREIGN KEY ("id_типа") REFERENCES public."ТипАвтомобиля"("id_типа");


--
-- TOC entry 4730 (class 2606 OID 16826)
-- Name: Сотрудник r_8; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Сотрудник"
    ADD CONSTRAINT r_8 FOREIGN KEY ("id_магазина") REFERENCES public."Магазин"("id_магазина");


--
-- TOC entry 4887 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2024-11-30 14:35:51

--
-- PostgreSQL database dump complete
--

