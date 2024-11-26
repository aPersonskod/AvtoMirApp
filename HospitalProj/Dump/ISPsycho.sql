--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2024-11-26 23:50:54

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
-- TOC entry 215 (class 1259 OID 16832)
-- Name: Анкета; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Анкета" (
    "Код_анкеты" integer NOT NULL,
    "Жалобы" character varying(50),
    "Проблема" character varying(50),
    "Цели_терапии" character varying(50),
    "Запрос" character varying(50),
    "Препятсвия" character varying(50),
    "Ценности" character varying(50),
    "Стремления" character varying(50),
    "Цели" character varying(50),
    "Код_пациента" integer
);


ALTER TABLE public."Анкета" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16835)
-- Name: Запись; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Запись" (
    "Код_записи" integer NOT NULL,
    "Дата_и_время_проведения" date,
    "Формат" character varying(20),
    "Код_пациента" integer,
    "Код_специалиста" integer,
    "Код_услуги" integer
);


ALTER TABLE public."Запись" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16838)
-- Name: Информация_о_встрече; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Информация_о_встрече" (
    "Код_встречи" integer NOT NULL,
    "Самочуствие" character varying(50),
    "Симптомы" character varying(50),
    "Интервенции" character varying(50),
    "Цитаты" character varying(50),
    "ДЗ" character varying(50),
    "Обратная_связь" character varying(50),
    "На_следующий_раз" character varying(50),
    "Впечатления" character varying(50),
    "Код_записи" integer
);


ALTER TABLE public."Информация_о_встрече" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16841)
-- Name: Пациент; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Пациент" (
    "Код_пациента" integer NOT NULL,
    "ФИО" character varying(30),
    "Пол" character varying(3),
    "Почта" character varying(20),
    "Статус" character varying(20),
    "Откуда" character varying(20),
    "Дата_рождения" date,
    "Номер_телефона" character varying(20)
);


ALTER TABLE public."Пациент" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16844)
-- Name: Специалист; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Специалист" (
    "Код_специалиста" integer NOT NULL,
    "ФИО" character varying(50),
    "Должность" character varying(20),
    "Телефон" character varying(20)
);


ALTER TABLE public."Специалист" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16847)
-- Name: Услуги; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Услуги" (
    "Код_услуги" integer NOT NULL,
    "Название_услуги" character varying(50),
    "Стоимость" integer
);


ALTER TABLE public."Услуги" OWNER TO postgres;

--
-- TOC entry 4868 (class 0 OID 16832)
-- Dependencies: 215
-- Data for Name: Анкета; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Анкета" ("Код_анкеты", "Жалобы", "Проблема", "Цели_терапии", "Запрос", "Препятсвия", "Ценности", "Стремления", "Цели", "Код_пациента") VALUES (1, 'Отсутствие концентрации', 'Мама обижает', 'Наладить отношение', 'Наладить рабочую сферу', 'Забор', 'Часы', 'Увеличить заработок', '200т.р.', 1);


--
-- TOC entry 4869 (class 0 OID 16835)
-- Dependencies: 216
-- Data for Name: Запись; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Запись" ("Код_записи", "Дата_и_время_проведения", "Формат", "Код_пациента", "Код_специалиста", "Код_услуги") VALUES (1, '2024-11-09', 'true', 1, 1, 1);


--
-- TOC entry 4870 (class 0 OID 16838)
-- Dependencies: 217
-- Data for Name: Информация_о_встрече; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Информация_о_встрече" ("Код_встречи", "Самочуствие", "Симптомы", "Интервенции", "Цитаты", "ДЗ", "Обратная_связь", "На_следующий_раз", "Впечатления", "Код_записи") VALUES (1, 'Хорошо', 'Гиперактивность', 'О боже мой', 'щас тебе найду цитаты', 'Фуух', 'Все понравилось', 'Посмотрим', 'Веселый чел', 1);


--
-- TOC entry 4871 (class 0 OID 16841)
-- Dependencies: 218
-- Data for Name: Пациент; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Пациент" ("Код_пациента", "ФИО", "Пол", "Почта", "Статус", "Откуда", "Дата_рождения", "Номер_телефона") VALUES (1, 'Тереньтьев М.П.', 'М', 'terenta345@gmail.com', 'Терапия', 'Реклама ВК', '1983-04-05', '+79546574512');


--
-- TOC entry 4872 (class 0 OID 16844)
-- Dependencies: 219
-- Data for Name: Специалист; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Специалист" ("Код_специалиста", "ФИО", "Должность", "Телефон") VALUES (1, 'Оптимус Прайм Аленович', 'Автобот', '+7354553');


--
-- TOC entry 4873 (class 0 OID 16847)
-- Dependencies: 220
-- Data for Name: Услуги; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Услуги" ("Код_услуги", "Название_услуги", "Стоимость") VALUES (1, 'Семейная консультация', 5000);
INSERT INTO public."Услуги" ("Код_услуги", "Название_услуги", "Стоимость") VALUES (2, 'Парная консультация', 5000);
INSERT INTO public."Услуги" ("Код_услуги", "Название_услуги", "Стоимость") VALUES (3, 'Индивидуальная консультация', 3000);


--
-- TOC entry 4709 (class 2606 OID 16851)
-- Name: Анкета Анкета_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Анкета"
    ADD CONSTRAINT "Анкета_pkey" PRIMARY KEY ("Код_анкеты");


--
-- TOC entry 4711 (class 2606 OID 16853)
-- Name: Запись Запись_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Запись"
    ADD CONSTRAINT "Запись_pkey" PRIMARY KEY ("Код_записи");


--
-- TOC entry 4713 (class 2606 OID 16855)
-- Name: Информация_о_встрече Информация_о_встрече_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Информация_о_встрече"
    ADD CONSTRAINT "Информация_о_встрече_pkey" PRIMARY KEY ("Код_встречи");


--
-- TOC entry 4715 (class 2606 OID 16857)
-- Name: Пациент Пациент_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Пациент"
    ADD CONSTRAINT "Пациент_pkey" PRIMARY KEY ("Код_пациента");


--
-- TOC entry 4717 (class 2606 OID 16859)
-- Name: Специалист Специалист_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Специалист"
    ADD CONSTRAINT "Специалист_pkey" PRIMARY KEY ("Код_специалиста");


--
-- TOC entry 4719 (class 2606 OID 16861)
-- Name: Услуги Услуги_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Услуги"
    ADD CONSTRAINT "Услуги_pkey" PRIMARY KEY ("Код_услуги");


--
-- TOC entry 4720 (class 2606 OID 16862)
-- Name: Анкета r_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Анкета"
    ADD CONSTRAINT r_1 FOREIGN KEY ("Код_пациента") REFERENCES public."Пациент"("Код_пациента");


--
-- TOC entry 4721 (class 2606 OID 16867)
-- Name: Запись r_2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Запись"
    ADD CONSTRAINT r_2 FOREIGN KEY ("Код_пациента") REFERENCES public."Пациент"("Код_пациента");


--
-- TOC entry 4722 (class 2606 OID 16872)
-- Name: Запись r_3; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Запись"
    ADD CONSTRAINT r_3 FOREIGN KEY ("Код_специалиста") REFERENCES public."Специалист"("Код_специалиста");


--
-- TOC entry 4723 (class 2606 OID 16877)
-- Name: Запись r_4; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Запись"
    ADD CONSTRAINT r_4 FOREIGN KEY ("Код_услуги") REFERENCES public."Услуги"("Код_услуги");


--
-- TOC entry 4724 (class 2606 OID 16882)
-- Name: Информация_о_встрече r_5; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Информация_о_встрече"
    ADD CONSTRAINT r_5 FOREIGN KEY ("Код_записи") REFERENCES public."Запись"("Код_записи");


--
-- TOC entry 4879 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2024-11-26 23:50:54

--
-- PostgreSQL database dump complete
--

