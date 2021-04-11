// vector.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

class vector {
    int _capacity = 4;
    int _capacity_inc = 4;
    int  _size = 0;
    int* _data = NULL;


public:

    class iterator {
        // указатель на узлы контейнра
        int* current = nullptr;

    public:
        iterator(int* n) {
            current = n;
        }

        bool operator != (iterator i) {
            return current != i.current;
        }

        void operator ++() {
            current++;
        }

        void operator ++(int) {
            current++;
        }

        int operator *() {
            return *current;
        }
    };
public:
    vector() {
        _data = (int*)malloc(_capacity * sizeof(int));
        if (_data == NULL)
            cout << "error malloc";
    }

    //vector v;
    void push_back(int key) {
        if (_capacity <= (_size + 1)) {
            _capacity += _capacity_inc;
            int* temp = (int*)malloc(_capacity * sizeof(int));
            if (temp == NULL)
                cout << "error malloc";
            memcpy(temp, _data, sizeof(int) * (_capacity - _capacity_inc));
            _data = temp;
        }
        *(_data + _size) = key;
        _size++;
    }
    iterator begin() {
        return iterator(_data);
    }

    iterator end() {
        return iterator(_data + _size); // ???
    }
    int size() {
        return _size;
    }
    int& operator[](int i) {
        return *(_data + i);
    }

    void insert(int i, int key)
    {
        _capacity += _capacity_inc;
        int* temp = (int*)malloc(_capacity * sizeof(int));
        if (temp == NULL)
            cout << "error malloc";
        memcpy(temp, _data, sizeof(int) * (_capacity - _capacity_inc));
        _data = temp;
        _size++;
        for (int j = _size; j >= i; j--) {
            temp[j] = _data[j - 1];
        }

        temp[i] = key;
        for (int j = i + 1; j >= 0; j--)
        {
            temp[j] = _data[j];
        }

        _data = temp;
    }

    void funcInsert(int*& List, int val, int posNum, int& size) { //Вставка элемента
        int* temp = (int*)malloc((size + 1) * sizeof(int));
        if (temp == NULL)
            cout << "error malloc";
        memcpy((void*)temp, (const void*)List, sizeof(int) * size);

        for (int i = size - 1; i >= posNum; i--) {
            temp[i + 1] = temp[i];
        }
        temp[posNum] = val;

        size++;

        if (temp != nullptr)
        {
            //List = new int[size];
            int* List = (int*)malloc(size * sizeof(int));
            if (List == NULL)
                cout << "error malloc";
            memcpy((void*)&List[0], (const void*)temp, sizeof(int) * size);
        }

        delete[] temp; temp = nullptr;
    }


    void removes() {
        // применить функцию free 
        free(_data);
        cout << endl << "removed";
    }



};

int main()
{
    vector v1;
    v1.push_back(1);
    v1.push_back(3);
    v1.push_back(2);
    v1.insert(2, -1);
    for (vector::iterator i = v1.begin(); i != v1.end(); i++) {
        std::cout << " { " << *i << " } ";
    }
    v1.removes();
    return 0;
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
