<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
  </div>
</template>
<script>
import add from "@/components/content_exam/Add_contentExamV.vue";
import list from "@/components/content_exam/List_contentExamV.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
  },
};
</script>
<style scoped></style>
