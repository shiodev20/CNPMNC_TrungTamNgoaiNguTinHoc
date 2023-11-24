<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
    <basedetail v-if="this.$route.query.action == 'detail'"></basedetail>
  </div>
</template>
<script>
import add from "@/components/exam/Add_Exam.vue";
import list from "@/components/exam/List_Exam.vue";
import detail from "@/components/exam/Detail_Exam.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
    basedetail: detail,
  },
};
</script>
<style scoped></style>
