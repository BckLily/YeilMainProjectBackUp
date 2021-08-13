#define _CRT_SECURE_NO_WARNINGS
#include<iostream>
#include<stdio.h>
#include "MyStack.h"

MyStack::MyStack()
{
	while (1)
	{
		printf("1)push 2)pop 3)all data 4) exit\n");
		int i;
		scanf("%d", &i);
		switch (i)
		{
		case 1:
			MyStack::push();
			break;
		case 2:
			MyStack::pop();
			break;
		case 3:
			MyStack::alldata();
			break;
		case 4:
			MyStack::exit();
			break;
		default:
			printf("Wrong INPUT\n\n");
			break;

		}
	}
}

void MyStack::push()
{
	// arrytop 현재 가장 마지막 데디터가 들어가있는
	// index번호 _ 들어가야하는 번호가 아닐때 주의
	if (arrytop < 9)
	{
		printf("숫자를 입력하세요\n\n");
		int a;  // 입력 숫자 저장 변수
		scanf("%d", &a);  // 입력한 숫자 입력
		printf("입력한 수 = %d\n\n", a); // 출력

		arrytop++; // 배열 자리를 다음 순서로 이동
		MyData[arrytop] = a; // arrytop : 헤더에서 선언한 배열 자리 초기값
		                     // 배열에 입력한 숫자 a를 저장한다.
		printf("arry[%d] = %d\n\n", arrytop, a); // 배열 자리와 저장된 숫자 출력
	}
	else
	{
		printf("Not Enough 스택 자리\n\n");
	}

}

void MyStack::pop()
{
	if (arrytop >= 0) // 배열에 데이터가 적어도 1개 이상은 들어가 잇는지 판단
	{
		printf("%d 에 있는 %d 를 pop 합니다\n\n", arrytop, MyData[arrytop]);
		arrytop--;
		//기존에 있던 마지막 데이터를 pop 했기에
		//가장 최근에 입력된 데이터는 arrytop 
	}
	else
	{
		printf("No Data\n\n");
	}

}

void MyStack::alldata()
{
	if (arrytop >= 0) 
	{
		for (int i = 0; i <= arrytop; i++)
		{
			printf("arry[%d] = %d\n",i, MyData[i]);
		}
		//배열은 0번부터 데이터가 저장되기 때문에
		//0번 부터 출력하며
		//마지막 데이터가 저장된 위치는 arrytop 위치이기 때문에
		//배열 끝까지가 아니라 arrytop번 까지만 출력해야한다.
	}
	else
	{
		printf("No Data\n\n");
	}
}

void MyStack::exit()
{
	return;
}
