#include <stdio.h>
#include <stdlib.h>

void add(int num, int** nums, int* count);
void print(int* nums, int count);
void remove1(int* nums, int* count);

int main()
{
    int choice, num, count;
    int* nums = NULL;
    count = 0;
    while (0 ==0)
    {
        printf("\nEnter 1.add num, 2.del last, 3.print all, 4.exit\n");
        scanf("%d", &choice);
        switch (choice) 
        {
            case 1:
                printf("Enter a num: \n");
                scanf("%d", &num);
                add(num, &nums, &count);
                break;
            case 2:
                remove1(nums, &count);
                break;
            case 3:
                print(nums, count);
                break;
            default:
                return 0;
                break;
      }
    }
    return 0;
}

void add(int num, int** nums, int* count)
{
    (*count)++;
    printf("Count: %d\n", *count);
    *nums = realloc(*nums, sizeof(int) * (*count));
    (*nums)[*count - 1] = num;
    return;
}

void remove1(int* nums, int* count)
{
    
    if (*count <= 0 || *count - 1 <0)
    {
        return;
    }
    (*count)--;
    nums = realloc(nums, *count);
    return;

}
void print(int* nums, int count)
{
    for(int i = 0; i < count; i++)
    {
        printf("%d ", nums[i]);
    }
}
