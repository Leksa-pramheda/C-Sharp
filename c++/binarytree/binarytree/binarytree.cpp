// binarytree.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

class btree {

    // узел дерева
    class node {
        node* left;
        node* right;
        node* parents;
        int key;
        friend class btree;

    };

public:

    node* root = nullptr;
    node* tempor = nullptr;

    int l_size = 0;
    int r_size = 0;
    int temp_size = 0;
    int _size = 0;
    class iterator {
        // указатель на узлы контейнра
        node* current = nullptr;
        node* temp = nullptr;

    public:
        iterator(node* n) {
            current = n;
        }

        bool operator != (iterator i) {
            return current != i.current;
        }

        void operator ++() {
            if (current->right) {
                temp = current->right;
                mini();
                current = temp;
            }
            else
                if (current->parents->right && current != current->parents->right)
                {
                    temp = current->parents->right;
                    mini();
                    current = temp;
                }

                else
                    current = current->parents;
        }

        void operator ++(int) {
            if (current->right) {
                temp = current->right;
                mini();
                current = temp;
            }
            else
                if (current->parents->right && current == current->parents->right)
                {
                    if (current->parents->parents->right != current->parents)
                        current = current->parents->parents;
                    else
                        current = nullptr;
                }
                else
                    current = current->parents;
        }

        void mini() {
            if (temp->left)
            {
                temp = temp->left;
                mini();
            }
        }

        int operator *() {
            return current->key;
        }
    };

    size_t size() {
        return (size_t)_size;
    }

    void push(int key, node* parent) {
        if (parent->key < key) {
            if (parent->right) {
                push(key, parent->right);
            }
            else {
                node* n = (node*)malloc(sizeof(node));
                if (n == NULL)
                    cout << "error malloc";
                parent->right = n;
                parent->right->key = key;
                parent->right->left = nullptr;
                parent->right->right = nullptr;
            }
            parent->right->parents = parent;
        }
        else {
            if (parent->left) {
                push(key, parent->left);
            }
            else {
                node* n = (node*)malloc(sizeof(node));
                if (n == NULL)
                    cout << "error malloc";
                parent->left = n;
                parent->left->key = key;
                parent->left->left = nullptr;
                parent->left->right = nullptr;
            }
            parent->left->parents = parent;
        }
        _size++;
    }

    void push(int key) {
        if (root) {
            push(key, root);
        }
        else {

            node* n = (node*)malloc(sizeof(node));
            if (n == NULL)
                cout << "error malloc";
            root = n;
            root->key = key;
            root->parents = nullptr;
            root->left = nullptr;
            root->right = nullptr;
        }
    }

    void print(node* parent) {
        if (parent->left)
            print(parent->left);

        std::cout << "{" << parent->key << "} ";

        if (parent->right)
            print(parent->right);
    }

    void print() {
        print(root);
    }

    bool search(int key, node* parent) {
        if (parent->key < key) {
            if (parent->right) {
                return search(key, parent->right);
            }
            else {
                return false;
            }
        }
        else {
            if (parent->key == key) {
                return true;
            }
            else {
                if (parent->left) {
                    return search(key, parent->left);
                }
                else {
                    return false;
                }
            }
        }
    }
    bool search(int key) {
        if (root) {
            return search(key, root);
        }
        return false;
    }

    void set_intersection(node* parent, btree& result, btree& other) {
        //перебор узлов текущего дерева
        if (parent->left)
            set_intersection(parent->left, result, other);
        if (other.search(parent->key))
            result.push(parent->key);
        if (parent->right)
            set_intersection(parent->right, result, other);


    }
    btree set_intersection(btree& other) {
        btree result;
        if (root) {
            set_intersection(root, result, other);
        }
        return result;
    }


    void set_union(node* parent, btree& result) {
        if (parent->left)
            set_union(parent->left, result);

        result.push(parent->key);

        if (parent->right)
            set_union(parent->right, result);
    }

    btree set_union(btree& other) {
        btree result;
        if (other.root) {
            set_union(other.root, result);
        }
        if (root)
            set_union(root, result);
        return result;
    }


    void set_diff(node* parent, btree& result, btree& other) {
        //перебор узлов текущего дерева
        if (parent->left)
            set_diff(parent->left, result, other);
        if (!other.search(parent->key))
            result.push(parent->key);
        if (parent->right)
            set_diff(parent->right, result, other);


    }

    btree set_diff(btree& other) {
        btree result;
        if (root)
            set_diff(root, result, other);
        return result;
    }

    iterator begin() {
        return min(root);
    }

    static iterator min(node* parent) {
        if (parent->left)
            return min(parent->left);
        return parent;
    }


    static iterator max(node* parent) {
        if (parent->right)
            return min(parent->right);
        return parent;
    }

    iterator end() {
        return iterator(nullptr);
    }

    void remove() {
        // применить функцию free       
        node* n = (node*)malloc(sizeof(node));
        if (n == NULL)
            cout << "error malloc";
        node* m = (node*)malloc(sizeof(node));
        if (m == NULL)
            cout << "error malloc";
        tempor = root;
        while (root != nullptr)
        {
            minim();

            n = tempor;
            vpered(n);
            free(n);
        }
        cout << "remove";
    }

    void removes() {
        l_size = 1;
        temp_size = 1;
        int control = 0;
        while (root->left && l_size > 0)
        {
            if (r_size == 0)
                l_size = 0;
            else temp_size = l_size;
            tempor = root->left;
            first();
            node* m = (node*)malloc(sizeof(node));
            if (m == NULL)
                cout << "error malloc";
            m = tempor->parents;
            free(tempor);
            tempor = m;
            if (r_size == 0 && l_size == 0)
                control++;
            r_size++;
            l_size--;
        }
        if (control == 0)
            free(root->left);
        l_size = 1;
        r_size = 0;
        temp_size = 1;
        control = 0;
        while (root->right && l_size > 0)
        {
            if (r_size == 0)
                l_size = 0;
            else temp_size = l_size;
            tempor = root->right;
            first();
            node* m = (node*)malloc(sizeof(node));
            if (m == NULL)
                cout << "error malloc";
            m = tempor->parents;
            free(tempor);
            tempor = m;
            if (r_size == 0 && l_size == 0)
                control++;
            l_size--;
            r_size++;
        }
        if (control == 0)
            free(root->right);
        free(root);


    }

    void first() {
        if (tempor->left && temp_size > 0)
        {
            tempor = tempor->left;
            if (r_size == 0)
                l_size++;
            else temp_size--;
            first();
        }
        else
            if (tempor->right && temp_size > 0)
            {
                tempor = tempor->right;
                if (r_size == 0)
                    l_size++;
                else temp_size--;
                first();
            }

    }

    void minim() {
        if (tempor->left)
        {
            tempor = tempor->left;
            minim();
        }
    }

    void vpered(node* n) {
        if (n->right) {
            tempor = n->right;
            mini();
            n = tempor;
        }
        else
            if (n->parents->right && n == n->parents->right)
            {
                if (n->parents->parents->right != n->parents)
                    n = n->parents->parents;
                else
                    n = nullptr;
            }
            else
                n = n->parents;
    }

    void mini() {
        if (tempor->left)
        {
            tempor = tempor->left;
            mini();
        }
    };
};

int main()
{
    btree b1;
    b1.push(5);
    b1.push(3);
    b1.push(4);
    b1.push(2);
    b1.push(9);
    b1.push(7);
    b1.push(12);

    b1.print();
    cout << endl;
    for (btree::iterator i = b1.begin(); i != b1.end(); i++)
        cout << " { " << *i << " } ";
    cout << endl;
    btree t;

    t.push(3);
    t.push(5);
    t.push(2);
    t.push(3);

    btree t2;

    t2.push(5);
    t2.push(2);
    t2.push(6);

    cout << "t1=";
    t.print();
    cout << endl;
    cout << "t2=";
    t2.print();
    cout << endl;
    cout << "Intersection" << endl;
    btree t3 = t.set_intersection(t2);
    t3.print();
    cout << endl << "Union" << endl;
    btree t4 = t.set_union(t2);
    t4.print();
    btree t5 = t.set_diff(t2);
    cout << endl << "Difference" << endl;
    t5.print();

    btree t6 = t2.set_diff(t);
    cout << endl << "Another difference" << endl;
    t6.print();

    setlocale(LC_ALL, "Russian");

    cout << endl << "Input key";
    int key = 0;
    cout << endl;
    cin >> key;

    if (t.search(key))
        cout << "Found";
    else
        cout << "Not found";
    t.removes();
    t2.removes();
    t3.removes();
    t4.removes();
    t5.removes();
    t6.removes();
    b1.removes();
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
