// list+free.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

class list {

    class node {
        int key;
        node* next = nullptr;

        friend class list;
    };

    node* head = nullptr;
    node* front = nullptr;
    node* n = nullptr;
    int _size = 0;
public:
    size_t sizes() {
        return (size_t)_size;
    }
    class malloc_free
    {
    public:
        static void* operator new(size_t size)
        {
            return malloc(size);
        }
        static void operator delete(void* p)
        {
            return free(p);
        }
    };
    class iterator {
        // указатель на узлы контейнра
        node* current = nullptr;

    public:
        iterator(node* n) {
            current = n;
        }

        int c = 0;
        bool operator != (iterator i) {
            return current != i.current;
        }

        void operator ++() {
            current = current->next;
        }

        void operator ++(int) {
            current = current->next;
        }

        int operator *() {
            return current->key;
        }


    };



    void push(int key) {

        node* n = (node*)malloc(sizeof(node));
        if (n==NULL)
            cout << "error malloc";
        n->key = key;
        if (head == nullptr)
        {
            
                head = n;

        }
        if (front != nullptr)
            front->next = n;

        front = n;
        _size++;
    }
    void remove() {
        // применить функцию free 
        node* n = (node*)malloc(sizeof(node));
        //n->key = key;
        if (n == NULL)
            cout << "error malloc";
        n = front->next;
        while(head->next !=n)
        {
            front = head->next;
            free(head);
            head = front;
        }
        free(head);
        cout << "removed";
    }

    void remove(int i) {
        node* n = head;
        node* m = NULL;
        int c = 0;
        if (i == 0)
            head = head->next;
        else {
            for (int j = 0; j < i - 1; j++)
            {
                n = n->next;
            }
            m = n;
            n = n->next;
            m->next = n->next;
        }
        _size--;
    }



    iterator begin() {
        return iterator(head);
    }

    iterator end() {
        return iterator(front->next); 
    }

};



int main()
{
    list l1;
    l1.push(1);
    l1.push(2);
    l1.push(3);

    for (list::iterator i = l1.begin(); i != l1.end(); i++) {
        std::cout << "{" << *i << "} ";
    }
    l1.remove();
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
