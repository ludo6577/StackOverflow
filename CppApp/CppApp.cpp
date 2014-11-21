
#include<cstdio>
#include<cstdlib>
#include<cstring>
#include<climits>
#include<iostream>

using namespace std;

void _perm(char *arr, char*result, int index)
{
	static int count = 1;
	if (index == strlen(arr))
	{
		cout << count++ << ". " << result << endl;
		return;
	}
	for (int i = 0; i < strlen(arr); i++)
	{
		result[index] = arr[i];
		_perm(arr, result, index + 1);
	}
}

int compare(const void *a, const void *b)
{
	return (*(char*)a - *(char*)b);
}



void perm(char *arr)
{
	int n = strlen(arr);
	if (n == 0)
		return;
	qsort(arr, n, sizeof(char), compare);
	char *data = new char[n];
	_perm(arr, data, 0);
	free(data);
	return;
}



int main()
{
	char arr[] = "BACD";
	perm(arr);
	return 0;
}

