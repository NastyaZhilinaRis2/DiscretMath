#include <iostream>
#include <string>
#include <vector>
#include <chrono>
#include <map>

char randomLetter() {
	int r = rand() % 37;
	char base = (r < 10) ? '0' : 'a';
	if (base == 0) return (char)(base + r % 10);
	else return (char)(base + r % 26);
}
bool CorrectInput()
{
	if (std::cin.fail())
	{
		std::cin.clear();
		std::cin.ignore(std::cin.rdbuf()->in_avail());
		return 0;
	}return 1;
}
void ClearingInputStream()
{
	std::cin.clear();
	std::cin.ignore(std::cin.rdbuf()->in_avail());
}
class Plenty
{
public:
	void SetHandUniversum();
	void SetAutoUniversum();
	void SetUniversum();
	void SetHandPlenty(Plenty& U);
	void SetAutoPlenty(Plenty& U);
	void SetPlenty(Plenty& U);
	void SetPlenty(std::vector<std::string> plenty);
	void SetSize(char namePlenty, int sizeU);
	int GetSize();
	std::vector<std::string> GetPlenty();
	void PrintPlenty();
	std::string IndexValue(int index);

private:
	int sizePlenty;
	std::vector<std::string> plenty;
};

void Plenty::SetHandUniversum()
{
	ClearingInputStream();
	std::string value;
	std::cout << "Введите элемент:\n";
	for (int i = 0; i < sizePlenty; i++)
	{
	start:
		std::cin >> value;
		for (int j = 0; j < sizePlenty; j++)
		{
			if (IndexValue(j) == value)
			{
				std::cout << "Данный элемент уже есть! Попробуйте ввести другой.\n";
				goto start;
			}

		}
		plenty.push_back(value);
	}
}
void Plenty::SetAutoUniversum()
{
	srand(time(0));
	std::string value;
	for (int i = 0; i < sizePlenty; i++)
	{
	start:
		value = randomLetter();
		for (int i = 0; i < sizePlenty; i++)
		{
			if (IndexValue(i) == value)
				goto start;
		}
		plenty.push_back(value);
	}
}
void Plenty::SetUniversum()
{
	int typeOfFilling;
	while (true)
	{
		std::cout << "Самостоятельное заполнение - 0, автозаполение - 1\n";
		std::cin >> typeOfFilling;
		if (!CorrectInput() || typeOfFilling != 0 && typeOfFilling != 1)
		{
			std::cout << "Неверно введен тип заполнения. Попробуйте еще раз.\n";
		}
		else
		{
			switch (typeOfFilling)
			{
			case 0:/*самостоятельное заполнение и вывод*/
			{
				SetHandUniversum();
				break;
			}
			case 1:/*автозаполнение*/
			{
				SetAutoUniversum();
				break;
			}
			}
			break;
		}
	}
}
void Plenty::SetHandPlenty(Plenty& U)
{
	ClearingInputStream();
	std::string value;
	for (int i = 0; i < sizePlenty; i++)
	{
		std::cout << "Введите элемент:\n";
	start:
		std::cin >> value;
		for (int j = 0; j < U.sizePlenty; j++)
		{
			if (U.IndexValue(j) == value)
			{
				for (int k = 0; k < sizePlenty; k++)
				{
					if (IndexValue(k) == value)
					{
						std::cout << "Данный элемент уже есть! Попробуйте ввести другой.\n";
						goto start;
					}
					else if (k == sizePlenty - 1) 
						break;
				}
				break;
			}
			else if (j == U.sizePlenty - 1)
			{
				std::cout << "Данного элемента нет! Попробуйте ввести другой.\n";
				goto start;
			}
		}
		plenty.push_back(value);
	}
}
void Plenty::SetAutoPlenty(Plenty& U)
{
	srand(time(0));
	std::vector<std::string> temp = U.GetPlenty();
	for (int i = 0; i < sizePlenty; i++)
	{
		int randIndex = rand() % temp.size();
		plenty.push_back(temp[randIndex]);
		temp.erase(temp.begin() + randIndex);
	}
}
void Plenty::SetPlenty(Plenty& U)
{
	int typeOfFilling;
	while (true)
	{
		std::cout << "Самостоятельное заполнение - 0, автозаполение - 1\n";
		std::cin >> typeOfFilling;
		if (!CorrectInput() || typeOfFilling != 0 && typeOfFilling != 1)
		{
			std::cout << "Неверно введен тип заполнения. Попробуйте еще раз.\n";
		}
		else
		{
			switch (typeOfFilling)
			{
			case 0:/*самостоятельное заполнение и вывод*/
			{
				SetHandPlenty(U);
				break;
			}
			case 1:/*автозаполнение*/
			{
				SetAutoPlenty(U);
				break;
			}
			default:/*иначе*/
			{
				std::cout << "Неверно введен тип заполнения. Попробуйте еще раз.\n";
				break;
			}
			}
			break;
		}
	}
}
void Plenty::SetPlenty(std::vector<std::string> plenty)
{
	sizePlenty = plenty.size();
	for (int i = 0; i < plenty.size(); i++)
	{
		this->plenty.push_back(plenty[i]);
	}
}
void Plenty::SetSize(char namePlenty, int sizeU)
{
	int size;
	
	while (true)
	{
		std::cout << "Размер " << namePlenty << ": ";
		std::cin >> size;
		if (!CorrectInput())
		{
			std::cout << "Неверный ввод. Попробуйте еще раз.\n";
		}
		else
		{
			if (size < 0)
				std::cout << "Размер не может быть меньше нуля!\n";
			else if (size == 0)
			{
				plenty.push_back("0");
				break;
			}
			else if (size > 36)
			{
				std::cout << "Размер не должен превышать 36!\n";
			}
			else if (!(sizeU >= size) && namePlenty != 'U')
			{
				std::cout << "Размер превышает размер универсума!\n";
			}
			else
			{
				ClearingInputStream();
				sizePlenty = size;
				break;
			}
		}
	}
}
int Plenty::GetSize()
{
	return sizePlenty;
}
std::vector<std::string> Plenty::GetPlenty()
{
	return plenty;
}

void Plenty::PrintPlenty()
{
	if (!plenty.empty())
	{
		std::cout << "{";
		for (int i = 0; i < sizePlenty; i++)
		{
			if (i != sizePlenty - 1) std::cout << plenty[i] << ", ";
			else std::cout << plenty[i];
		}
		std::cout << "}\n\n";
	}
	else std::cout << "{}\n\n";
}

std::string Plenty::IndexValue(int index)
{
	if (plenty.size() > index)
		return plenty[index];
	else return "пустой";
}
//пересечение
std::vector<std::string> Intersection(Plenty& A, Plenty& B)
{
	std::vector<std::string> C;
	for (int i = 0; i < A.GetSize(); i++)
	{
		for (int j = 0; j < B.GetSize(); j++)
		{
			if (A.IndexValue(i) == B.IndexValue(j))
			{
				C.push_back(A.IndexValue(i));
				break;
			}
		}
	}
	return C;
}
//дополнение
std::vector<std::string> Addition(Plenty& A, Plenty& U)
{
	std::vector<std::string> B;
	if (A.GetSize() == 0)
	{
		for (int i = 0; i < U.GetSize(); i++)
		{
			B.push_back(U.IndexValue(i));
		}
	}
	else if (A.GetSize() != U.GetSize())
	{
		for (int i = 0; i < U.GetSize(); i++)
		{
			for (int j = 0; j < A.GetSize(); j++)
			{
				if (A.IndexValue(j) == U.IndexValue(i))
				{
					break;
				}
				else if (j == A.GetSize() - 1)
				{
					B.push_back(U.IndexValue(i));
				}
			}
		}
	}
	return B;
}
//вычитание
std::vector<std::string> Subtraction(Plenty& A, Plenty& B, Plenty& U)
{
	Plenty notB;
	notB.SetPlenty(Addition(B, U));
	std::vector<std::string> C;
	C = Intersection(A, notB);
	return C;
}
//объединение
std::vector<std::string> Union(Plenty& A, Plenty& B, Plenty& U)
{
	std::vector<std::string> C;
	C = Subtraction(A, B, U);
	for (int i = 0; i < B.GetSize(); i++)
	{
		C.push_back(B.IndexValue(i));
	}
	return C;
}

//симметрич.вычитание
std::vector<std::string> SymmetricSubtraction(Plenty& A, Plenty& B, Plenty& U)
{
	Plenty SubtrAAndB;
	SubtrAAndB.SetPlenty(Subtraction(A, B, U));

	Plenty SubtrBAndA;
	SubtrBAndA.SetPlenty(Subtraction(B, A, U));
	std::vector<std::string> C;
	C = Union(SubtrAAndB, SubtrBAndA, U);
	return C;
}
//вхождение элемента в множество
int OccurrenceElement(Plenty& A, std::string element)
{
	for (int i = 0; i < A.GetSize(); i++)
	{
		if (A.IndexValue(i) == element)
			return 1;
	}
	return 0;
}

//вхождение одного множества в другое
int OccurrencePlenty(Plenty& A, Plenty& B)
{
	int coincidences = 0;
	for (int i = 0; i < A.GetSize(); i++)
	{
		for (int j = 0; j < B.GetSize(); j++)
		{
			if (A.IndexValue(i) == B.IndexValue(j))
			{
				coincidences++;
				break;
			}
		}
	}
	if (coincidences == A.GetSize()) return 1;
	else return 0;
}

void Calc()
{
	std::cout << "Для начала работы калькулятора необходимо ввести универсальное множество U,\nа так же множества A, B, C, D, E\n\n";
	Plenty U;
	int sizeU = 0;
	U.SetSize('U', sizeU);
	U.SetUniversum();
	U.PrintPlenty();
	sizeU = U.GetSize();

	Plenty A;
	A.SetSize('A', sizeU);
	A.SetPlenty(U);
	A.PrintPlenty();

	Plenty B;
	B.SetSize('B', sizeU);
	B.SetPlenty(U);
	B.PrintPlenty();

	Plenty C;
	C.SetSize('C', sizeU);
	C.SetPlenty(U);
	C.PrintPlenty();

	Plenty D;
	D.SetSize('D', sizeU);
	D.SetPlenty(U);
	D.PrintPlenty();

	Plenty E;
	E.SetSize('E', sizeU);
	E.SetPlenty(U);
	E.PrintPlenty();

	int fOnCalc;
	do
	{
		int numOperation;
		std::string plenty1, plenty2;

		// Создаем контейнер для хранения экземпляров классов
		std::map<std::string, Plenty> myPlents;
		myPlents["A"] = A;
		myPlents["B"] = B;
		myPlents["C"] = C;
		myPlents["D"] = D;
		myPlents["E"] = E;
		myPlents["U"] = U;

		std::cout << "\nВыберите операцию для множеств:\n1. Пересечение\n2. Объединение\n3. Разность\n4. Симметрическая разность\n5. Дополнение\n6. Принадлежность элемента множеству\n7. Вхождение одного подмножества в другое\n\n";
	
	startChooseOperation:
		std::cin >> numOperation;
		if (!CorrectInput())
		{
			std::cout << "Неверный ввод. Попробуйте еще раз.\n";
			goto startChooseOperation;
		}
		else if (numOperation > 7 || numOperation < 1)
			goto startChooseOperation;

		switch (numOperation)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 7:
		{
			std::cout << "Выберите ДВА множества (A-E, U):\n";
			std::cout << "Первое: ";
			startChoosePlenty1:
			std::cin >> plenty1;
			if (plenty1 != "A" && plenty1 != "B" && plenty1 != "C" && plenty1 != "D" && plenty1 != "E" && plenty1 != "U")
			{
				std::cout << "Неверный ввод. Попробуйте еще раз.\n";
				goto startChoosePlenty1;
			}
			myPlents[plenty1].PrintPlenty();
			std::cout << "Второе: ";
		}
		case 5:
		case 6:
		{
			if(numOperation == 5 || numOperation == 6)
			std::cout << "Выберите множество (A-E, U):\n";
		startChoosePlenty2:
			std::cin >> plenty2;
			if (plenty2 != "A" && plenty2 != "B" && plenty2 != "C" && plenty2 != "D" && plenty2 != "E" && plenty2 != "U")
			{
				std::cout << "Неверный ввод. Попробуйте еще раз.\n";
				goto startChoosePlenty2;
			}
			myPlents[plenty2].PrintPlenty();
			break;
		}
		}
		

		Plenty C;
		switch (numOperation)
		{
		case 1:
		{
			C.SetPlenty(Intersection(myPlents[plenty1], myPlents[plenty2]));
			std::cout << "Результат пересечения:\n";
			C.PrintPlenty();
			break;
		}
		case 2:
		{
			C.SetPlenty(Union(myPlents[plenty1], myPlents[plenty2], U));
			std::cout << "Результат объединения:\n";
			C.PrintPlenty();
			break;
		}
		case 3:
		{
			C.SetPlenty(Subtraction(myPlents[plenty1], myPlents[plenty2], U));
			std::cout << "Результат разности:\n";
			C.PrintPlenty();
			break;
		}
		case 4:
		{
			C.SetPlenty(SymmetricSubtraction(myPlents[plenty1], myPlents[plenty2], U));
			std::cout << "Результат симметрической разности:\n";
			C.PrintPlenty();
			break;
		}
		case 5:
		{
			C.SetPlenty(Addition(myPlents[plenty2], U));
			std::cout << "Результат дополнения:\n";
			C.PrintPlenty();
			break;
		}
		case 6:
		{
			std::string element;
			std::cin >> element;
			if(OccurrenceElement(myPlents[plenty2], element))
				std::cout << "Элемент входит в множество.\n";
			else
				std::cout << "Элемент НЕ входит в множество.\n";
			break;
		}
		case 7:
		{
			if (OccurrencePlenty(myPlents[plenty1], myPlents[plenty2]))
				std::cout << "Первое множество - подмножество второго.\n";
			else
				std::cout << "Первое множество - НЕ подмножество второго.\n";
			break;
		}
		}

		std::cout << "Для завершения работы с калькулятором нажмите 0, иначе 1\n";
	start4:
		std::cin >> fOnCalc;
		if (!CorrectInput())
		{
			std::cout << "Неверный ввод. Попробуйте еще раз.\n";
			goto start4;
		}
		switch (fOnCalc)
		{
		case 0:
			fOnCalc = 0;
			break;
		case 1:
			fOnCalc = 1;
			break;
		default:
			std::cout << "Неверный ввод. Попробуйте еще раз.\n";
			goto start4;
		}

	} while (fOnCalc != 0);
}
int main()
{
	setlocale(LC_ALL, "Rus");
	Calc();
}