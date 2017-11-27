В текстовом файле построчно хранится информация о URL-адресах, представленных в виде
"scheme://host/URL‐path?parameters", где сегмент parameters - это набор пар вида key=value,
при этом сегменты URL‐path и parameters  или сегмент parameters могут отсутствовать. 

Разработать систему типов (руководствоваться принципами SOLID) для экспорта данных,
полученных на основе разбора информации текстового файла, в XML-документ по определенному правилу.
Можно использовать любую XML технологию без ограничений.
 
Для тех URL-адресов, которые не совпадают с данным паттерном, “залогировать” информацию, отметив указанные строки,
как необработанные.

Продемонстрировать работу на примере консольного приложения.