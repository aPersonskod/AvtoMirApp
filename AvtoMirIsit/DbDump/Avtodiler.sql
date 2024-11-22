--
-- PostgreSQL database dump
--

-- Dumped from database version 13.15 (Raspbian 13.15-0+deb11u1)
-- Dumped by pg_dump version 16.0

-- Started on 2024-10-26 17:51:28

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
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 200 (class 1259 OID 17385)
-- Name: Автомобиль; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Автомобиль" (
    "id_автомобиля" integer NOT NULL,
    "Номер" integer,
    "vin_номер" character varying(20),
    "Год_выпуска" date,
    "Цена" integer,
    "Цвет" character varying(20),
    "id_типа" integer,
	"photo" character varying(300)
);


ALTER TABLE public."Автомобиль" OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 17390)
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
-- TOC entry 202 (class 1259 OID 17395)
-- Name: Клиент; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Клиент" (
    "id_клиента" integer NOT NULL,
    "ФИО" character varying(20),
    "Адрес" character varying(20),
    "Телефон" integer
);


ALTER TABLE public."Клиент" OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 17400)
-- Name: Магазин; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Магазин" (
    "id_магазина" integer NOT NULL,
    "Адрес" character varying(20)
);


ALTER TABLE public."Магазин" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 17405)
-- Name: МаркаАвтомобиля; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."МаркаАвтомобиля" (
    "id_марка" integer NOT NULL,
    "Марка" character varying(20)
);


ALTER TABLE public."МаркаАвтомобиля" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 17410)
-- Name: Сотрудник; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Сотрудник" (
    "id_сотрудника" integer NOT NULL,
    "ФИО" character varying(20),
    "Телефон" integer,
    "id_магазина" integer
);


ALTER TABLE public."Сотрудник" OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 17415)
-- Name: ТипАвтомобиля; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ТипАвтомобиля" (
    "id_типа" integer NOT NULL,
    "Модель" character varying(20),
    "id_марка" integer
);


ALTER TABLE public."ТипАвтомобиля" OWNER TO postgres;

--
-- TOC entry 3025 (class 0 OID 17385)
-- Dependencies: 200
-- Data for Name: Автомобиль; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3026 (class 0 OID 17390)
-- Dependencies: 201
-- Data for Name: Договор; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3027 (class 0 OID 17395)
-- Dependencies: 202
-- Data for Name: Клиент; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3028 (class 0 OID 17400)
-- Dependencies: 203
-- Data for Name: Магазин; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3029 (class 0 OID 17405)
-- Dependencies: 204
-- Data for Name: МаркаАвтомобиля; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3030 (class 0 OID 17410)
-- Dependencies: 205
-- Data for Name: Сотрудник; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3031 (class 0 OID 17415)
-- Dependencies: 206
-- Data for Name: ТипАвтомобиля; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 2876 (class 2606 OID 17389)
-- Name: Автомобиль Автомобиль_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Автомобиль"
    ADD CONSTRAINT "Автомобиль_pkey" PRIMARY KEY ("id_автомобиля");


--
-- TOC entry 2878 (class 2606 OID 17394)
-- Name: Договор Договор_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT "Договор_pkey" PRIMARY KEY ("id_договора");


--
-- TOC entry 2880 (class 2606 OID 17399)
-- Name: Клиент Клиент_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Клиент"
    ADD CONSTRAINT "Клиент_pkey" PRIMARY KEY ("id_клиента");


--
-- TOC entry 2882 (class 2606 OID 17404)
-- Name: Магазин Магазин_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Магазин"
    ADD CONSTRAINT "Магазин_pkey" PRIMARY KEY ("id_магазина");


--
-- TOC entry 2884 (class 2606 OID 17409)
-- Name: МаркаАвтомобиля МаркаАвтомобиля_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."МаркаАвтомобиля"
    ADD CONSTRAINT "МаркаАвтомобиля_pkey" PRIMARY KEY ("id_марка");


--
-- TOC entry 2886 (class 2606 OID 17414)
-- Name: Сотрудник Сотрудник_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Сотрудник"
    ADD CONSTRAINT "Сотрудник_pkey" PRIMARY KEY ("id_сотрудника");


--
-- TOC entry 2888 (class 2606 OID 17419)
-- Name: ТипАвтомобиля ТипАвтомобиля_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ТипАвтомобиля"
    ADD CONSTRAINT "ТипАвтомобиля_pkey" PRIMARY KEY ("id_типа");


--
-- TOC entry 2890 (class 2606 OID 17425)
-- Name: Договор r_3; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT r_3 FOREIGN KEY ("id_сотрудника") REFERENCES public."Сотрудник"("id_сотрудника");


--
-- TOC entry 2891 (class 2606 OID 17430)
-- Name: Договор r_4; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT r_4 FOREIGN KEY ("id_автомобиля") REFERENCES public."Автомобиль"("id_автомобиля");


--
-- TOC entry 2892 (class 2606 OID 17435)
-- Name: Договор r_5; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Договор"
    ADD CONSTRAINT r_5 FOREIGN KEY ("id_клиента") REFERENCES public."Клиент"("id_клиента");


--
-- TOC entry 2894 (class 2606 OID 17445)
-- Name: ТипАвтомобиля r_6; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ТипАвтомобиля"
    ADD CONSTRAINT r_6 FOREIGN KEY ("id_марка") REFERENCES public."МаркаАвтомобиля"("id_марка");


--
-- TOC entry 2889 (class 2606 OID 17420)
-- Name: Автомобиль r_7; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Автомобиль"
    ADD CONSTRAINT r_7 FOREIGN KEY ("id_типа") REFERENCES public."ТипАвтомобиля"("id_типа");


--
-- TOC entry 2893 (class 2606 OID 17440)
-- Name: Сотрудник r_8; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Сотрудник"
    ADD CONSTRAINT r_8 FOREIGN KEY ("id_магазина") REFERENCES public."Магазин"("id_магазина");


--
-- TOC entry 3037 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2024-10-26 17:51:29

--
-- PostgreSQL database dump complete
--

